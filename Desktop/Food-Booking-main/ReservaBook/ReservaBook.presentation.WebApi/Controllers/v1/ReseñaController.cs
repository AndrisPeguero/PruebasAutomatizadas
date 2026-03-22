using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.Reseña;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Cliente")]
    public class ReseñaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IReseñaService service;


        public ReseñaController(IMapper _mapper, IReseñaService service)
        {
            this.service = service;
            this._mapper = _mapper;
        }



        [HttpGet("get-all-resenia/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReseñaResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByIdRestaurante([FromRoute] GetReseñasByIdRestauranteRequestDto dto)
        {
            try
            {
                if (dto == null)
                {

                    return BadRequest("debes indicar el restaurante para ver su reseñas");
                
                }

                var entities = await service.
                    GetAllByIdRestaurnteAsync(dto.Id);


                if (entities == null || entities.Count <= 0)
                {
                    foreach(var item in entities!)
                    {

                        if (item!.HasErrors)
                        {
                            return NotFound(item.Errors);
                        }

                    }
                
                }


                return Ok(entities);


            }
            catch (Exception ex)
            {
            
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
           
            }
        }



        [HttpPost("add-reseña")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReseñaResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] SaveReseñaRequestDto dto)
        {
            try
            {

                var userId = User.FindFirst("UId")!.Value;


                if (dto == null)
                {

                    return BadRequest("debes indicar el restaurante para ver su reseñas");

                }


                var map = _mapper.Map<CreateReseñaDto>(dto);
                map.ClienteId = userId;
                var entity = await service.
                    AddAsync(map);

                if(entity == null || entity.HasErrors)
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




        [HttpDelete("test-delete/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteReseñaRequestDto dto)
        {

            try
            {
                if (dto == null)
                {

                    return BadRequest("debes indicar el restaurante para eliminarlo");
                }


           
                var entity = await service.
                    DeleteAsync(dto.Id);


                if (!entity)
                {
                    return NotFound("No se encontro la reseña especificada, favor revisar");
                }


                return NoContent();

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }




        [HttpPut("update-reseña/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReseñaResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody] UpdateReseñaRequestDto dto)
        {

            try
            {
                var userId = User.FindFirst("UId")!.Value;

                if (dto == null)
                {

                    return BadRequest("debes introduccir los valores correctamente para actualizar, favor verificar");

                }

                if(id <= 0)
                {

                    return BadRequest("debes indicar el restaurante para eliminarlo");

                }



                var map = _mapper.Map<CreateReseñaDto>(dto);
                map.ClienteId = userId;
                var entity = await service.
                    UpdateAsync(id,map);


                if (entity == null || entity.HasErrors)
                {


                    return BadRequest(entity!.Errors);
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
