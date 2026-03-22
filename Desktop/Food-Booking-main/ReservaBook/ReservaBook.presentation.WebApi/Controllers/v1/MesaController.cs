using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Common.Enums;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [Authorize(Roles = "Propietario")]
    public class MesaController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IMesaService _mesaService;



        public MesaController(IMapper _mapper, IMesaService _mesaService)
        {

            this._mapper = _mapper;
            this._mesaService = _mesaService;


        }




        [HttpGet("Get-all-mesa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var entities = await _mesaService.GetlAllAsync();

                if (entities == null || entities.Count == 0)
                {

                    return NotFound("No se encontraron mesas registrada");

                }

                return Ok(entities);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }





        [HttpPost("Add-mesa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddAsync([FromBody] SaveMesaRequestDto request)
        {

            try
            {
                var userID = User.FindFirst("UId")!.Value;
                var dtoCreate = _mapper.Map<CreateMesaRequestDto>(request);
                dtoCreate.UsurioId = userID;
                dtoCreate.Estado = Estado.Disponible.ToString();
                var entity = await _mesaService.AddAsync(dtoCreate);

                if (entity!.HasError || entity == null)
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



        [HttpPost("change-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusMesaRequestDto request)
        {

            try
            {

                if (request == null)
                {
                    return BadRequest("Debes indicar valores valido para cambiar el estado");
                }


                var entity = await _mesaService.ChangeStatus(request.IdMesa,request.Status);

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






        [HttpPost("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete([FromRoute] MesaRequestIdDto dto)
        {

            try
            {

                var entity = await _mesaService.DeleteAsync(dto.Id);


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




        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetById([FromRoute] MesaRequestIdDto dto)
        {

            try
            {
                var entity = await _mesaService.GetByIdAsync(dto.Id);


                if (entity == null || entity.HasError)
                {

                    return NotFound(entity!.Errors);

                }


                return Ok(entity);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }






        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateMesa(int id, [FromBody] UpdateMesaRequestDto request)
        {

            try
            {

                if (request == null)
                {
                    return BadRequest("Debes llena los campos correctamente favor verificar");
                }


                var map = _mapper.Map<CreateMesaRequestDto>(request);
                var entity = await _mesaService.UpdateAsync(id, map);


                if (entity == null || entity.HasError)
                {

                    return NotFound(entity!.Errors);

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
