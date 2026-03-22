using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.presentation.WebApi.Handlers;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
   
    [ApiVersion("1.0")]
    [Authorize(Roles = "Propietario")]
    public class RestauranteController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IRestauranteServices _restauranteServices;


        public RestauranteController(IRestauranteServices _restauranteServices, IMapper mapper)
        {

            this._restauranteServices = _restauranteServices;
            this._mapper = mapper;

        }





        [HttpGet("GetAll-restaurante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRestaurante()
        {
            try
            {

                var result = await _restauranteServices.GetlAllAsync();

                if (result.Count == 0 || result == null)
                {
                    return NotFound("No se contraron restaurante registrado");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }




        [HttpPost("add-restaurante")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestauranteResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRestaurante([FromForm] SaveRestauranteRequestDto saveRestaurante)
        {
            try
            {

                var userId = User.FindFirst("UId")!.Value;
                if (saveRestaurante == null)
                {
                    return BadRequest("Debes enviar la informacion correctamente, favor validar");

                }

                var map = _mapper.Map<CreateRestauranteRequestDto>(saveRestaurante);
                map.Id = 0;
                map.UsuarioId = userId; 
                map.Imagen = FileHandler.Upload(saveRestaurante.Imagen, userId, "", false, "");
                var result = await _restauranteServices.AddAsync(map);


                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }


        [HttpPost("Delete-restaurante/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRestaurante(int id)
        {
            try
            {
                if(id <= 0) 
                {
                    return BadRequest("Debes indicar un id valido, favor verificar");
                
                }


                var result = await _restauranteServices.DeleteRestauranteAsync(id);

                if (result == null || !result.Success)
                {
                    return NotFound("No se contro un recurso con ese id");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }




        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdRestaurante(int id)
        {
            try
            {

                var result = await _restauranteServices.GetByIdAsync(id);

                if (result == null || result.HasError)
                {
                    return NotFound(result!.Errors);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }




        [HttpPut("Update-restaurante/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestauranteResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRestaurante(int id, [FromForm] UpdateRestauranteRequestDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Debes enviar los valores correctamente, favor verificar");
                }

                var userId = User.FindFirst("UId")!.Value;
                var map = _mapper.Map<CreateRestauranteRequestDto>(dto);


                map.Imagen = FileHandler.Upload(dto.Imagen, userId, "", true, "Restaurantes")!;
                map.UsuarioId = userId;
                var result = await _restauranteServices.UpdateAsync(id, map);

                if (result == null || result.HasError)
                {
                    return NotFound(result!.Errors);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }
































    }
}
