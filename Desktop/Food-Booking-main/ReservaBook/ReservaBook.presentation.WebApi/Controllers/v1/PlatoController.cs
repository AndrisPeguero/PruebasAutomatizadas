using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.plato;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.presentation.WebApi.Handlers;



namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Propietario")]
    public class PlatoController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IPlatoService _PlatoService;



        public PlatoController(IMapper mapper, IPlatoService platoService)
        {
            _mapper = mapper;
            _PlatoService = platoService;
        }



        [HttpGet("get-All-Plato")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PlatoResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {

            try
            {

                var entities = await _PlatoService.GetlAllAsync();


                if (entities.Count == 0 || entities == null)
                {

                    return NotFound("No se encontrar platos registrado, favor verificar");
                
                
                }

                return Ok(entities);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            
           
            }
        }




        [HttpPost("add-plato")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsyn([FromForm] SavePlatoRequestDto request)
        {

            try
            {
                if (request == null)
                {

                    return BadRequest("Debes ingresar los valores correctamente, favor verificar");

                }


                var userId = User.FindFirst("UId")!.Value;
                var map = _mapper.Map<CreatePlatoRequestDto>(request);
                map.Estado = Estado.Disponible.ToString();
                map.Imagen = FileHandler.Upload(request.Imagen,userId,"",false,"Platos");
                var entities = await _PlatoService.AddAsync(map);

   
                return Created("",entities);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }



        [HttpPost("change-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusPlatoRequestDto request)
        {

            try
            {

                if (request == null)
                {
                    return BadRequest("Debes indicar valores valido para cambiar el estado");
                }


                var entity = await _PlatoService.ChangeStatus(request.IdPlato, request.Status);

                if (!entity)
                {

                    return NotFound("No se encontro una mesa con ese id, favor verificar");

                }


                return NoContent();

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }





        [HttpPut("update-plato/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsyn(int id,[FromForm] UpdatePlatoRequestDto request)
        {

            try
            {
                if (request == null)
                {

                    return BadRequest("Debes ingresar los valores correctamente, favor verificar");

                }


                var userId = User.FindFirst("UId")!.Value;
                var map = _mapper.Map<CreatePlatoRequestDto>(request);
                map.Imagen = FileHandler.Upload(request.Imagen, userId, "", true, "Platos");
                var entities = await _PlatoService.UpdateAsync(id,map);

                if(entities!.HasError)
                {
                    return NotFound(entities.Errors);
                }


                return Ok(entities);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }




        [HttpPost("delete-plato/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsyn([FromRoute] PlatoIdRequestDto request)
        {

            try
            {
             
                var entities = await _PlatoService.DeleteAsync(request.Id);

                if (!entities)
                {
                    return NotFound("No se encontro un plato con ese id, favor verificar");
                }


                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }




        [HttpGet("getById/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsyn([FromRoute] PlatoIdRequestDto request)
        {

            try
            {

                var entity = await _PlatoService.GetByIdAsync(request.Id);

                if (entity!.HasError)
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

























































    }
}
