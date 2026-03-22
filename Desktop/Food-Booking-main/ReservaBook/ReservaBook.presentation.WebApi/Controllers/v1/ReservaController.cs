using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.reserva;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Cliente")]
    public class ReservaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IReservaRestauranteService _service;


        public ReservaController(IMapper _mapper, IReservaRestauranteService _service)
        {
            this._mapper = _mapper;
            this._service = _service;
        }




        [HttpGet("get-all-reservas")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entiites = await _service.GetlAllAsync();

                if (entiites == null || entiites.Count == 0)
                {

                    return NotFound("No se encontraron reservas registrada");
                
                }

                return Ok(entiites);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
         
            }
        }



        [HttpGet("get-all-By-UsuarioId")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByUsuarioId()
        {
            try
             {
                var userId = User.FindFirst("UId")!.Value;

                var entiites = await _service.GetAllReservaByIdUsuario(userId);
                
                if (entiites == null || entiites.Count == 0)
                {

                    return NotFound("No se encontraron reservas registrada para el usuario indicado");

                }

                return Ok(entiites);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpPost("add-reservas")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservaResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] SaveReservaRequestDto dto)
        {
            try
            {    if(dto == null)
                {
                    return BadRequest("Debes indicar valores validos para realizar la reserva, favor verificar");
                }



                var userId = User.FindFirst("UId")!.Value;


                var map = _mapper.Map<CreateReservaRequestDto>(dto);
                map.IdUsuario = userId;
                var entity = await _service.AddAsync(map);


                if (entity == null || entity.HasError)
                {

                    return BadRequest(entity!.Errors);

                }

                return Created("",entity);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }





        [HttpPost("update-reservas/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservaResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody] UpdateReservaRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes indicar valores validos para  actualizar la reserva, favor verificar");
                }



                var userId = User.FindFirst("UId")!.Value;


                var map = _mapper.Map<CreateReservaRequestDto>(dto);
                var entity = await _service.UpdateAsync(id,map);


                if (entity == null || entity.HasError)
                {

                    return BadRequest(entity!.Errors);

                }

                return Created("", entity);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }




        [HttpPost("delete-reservas/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteReservaRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes indicar la reserva que quieres eliminar");
                }



                var entity = await _service.DeleteAsync(dto.Id);



                if (!entity)
                {
                    return NotFound("No se encontro la reserva que se quire eliminar");
                }


                return NoContent();

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
















    }
}
