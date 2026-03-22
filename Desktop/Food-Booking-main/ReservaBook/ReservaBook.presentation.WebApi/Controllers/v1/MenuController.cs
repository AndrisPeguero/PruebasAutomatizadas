using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.menu;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Propietario")]
    public class MenuController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMenuService menuService;



        public MenuController(IMapper _mapper, IMenuService menuService)
        {
        
            this._mapper = _mapper;
            this.menuService = menuService;


        }




        [HttpGet("get-menus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await menuService.GetlAllAsync();

                if(entities == null || entities.Count <= 0) 
                {

                    return NotFound("No se encontro menu registrado en el sistema, favor verificar");  
                
                }



                return Ok(entities);


            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            
            
            }
        }






        [HttpPost("add-menu")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMenu([FromBody] SaveMenuRequestDto dto)
        {
            try
            {
                if(dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente, favor verificar");
                }

                var userId = User.FindFirst("UId").Value;
                var map = _mapper.Map<CreateMenuDto>(dto);
                map.IdUsuario = userId;
                var entity = await menuService.AddAsync(map);

                if(entity == null)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError);

                }

                return Ok(entity);


            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }





        [HttpPut("update-menu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMenu(int id,[FromBody] UpdateMenuRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente, favor verificar");
                }




                var map = _mapper.Map<CreateMenuDto>(dto);
                var entity = await menuService.UpdateAsync(id,map);

                if (entity.HasErrors)
                {

                    return NotFound(entity);

                }

                return Ok(entity);


            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }





        [HttpPost("delete-menu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMenu([FromRoute] MenuIdRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes introduccir un id valido , favor verificar");
                }



                var entity = await menuService.DeleteAsync(dto.Id);
                if (!entity)
                {

                    return NotFound("No se encontro un menu con ese id, favor verificar");

                }

                return NoContent();


            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }





        [HttpGet("get-byId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdMenu([FromRoute] MenuIdRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes introduccir un id valido , favor verificar");
                }



                var entity = await menuService.GetByIdAsync(dto.Id);
                if (entity == null)
                {

                    return NotFound("No se encontro un menu con ese id, favor verificar");

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
