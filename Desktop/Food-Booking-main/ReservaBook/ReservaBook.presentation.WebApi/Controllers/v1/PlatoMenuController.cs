using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.platoMenu;
using ReservaBook.Core.Aplication.Interfaces;
using System.Threading.Tasks;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Propietario")]
    public class PlatoMenuController : BaseApiController
    {
        private readonly IPlatoMenuServices _service;



        public PlatoMenuController(IPlatoMenuServices _service) 
        {

            this._service = _service;
        
        }



        [HttpPost("Add-plato-all-menu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatoMenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPlatoAlMenu([FromBody] SavePlatoMenuRequesDto dto)
        {
            try
            {
                if(dto == null)
                {
                    return BadRequest("Debes indicar un plato y un menu  valido");
                }


                var entities = await _service.AddPlatoAlMenu(dto.IdMenu,dto.IdPlatos);
                foreach (var entity in entities)
                {

                    if (entity.HasError)
                    {
                        return NotFound(entity.Errors);

                    }
                }
               


                return Ok(entities);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            
            }
         
        }




        [HttpDelete("delete-plato-del-menu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatoMenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlatoDelMenu([FromRoute] DeletePlatoMenuRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes indicar un plato y un menu  valido");
                }


                var entity = await _service.DeletePlatoDelMenu(dto.IdMenu,dto.IdPlato);

                if (entity.HasError)
                {
                    return NotFound(entity.Errors);

                }


                return Ok(entity);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }




        [HttpGet("getAll-platomenu")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlatoMenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                


                var entity = await _service.GetlAllAsync();

                if (entity == null)
                {
                    return NotFound("No se encontraron plato en un menu");

                }


                return Ok(entity);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }







    }

}
