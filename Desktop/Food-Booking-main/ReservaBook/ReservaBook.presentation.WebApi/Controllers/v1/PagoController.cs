using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.pago;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Cliente")]
    public class PagoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPagoService service;




        public PagoController(IMapper _mapper, IPagoService service)
        {
            this._mapper = _mapper;
            this.service = service; 

        }



        [HttpPost("add-pago")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PagoResponseDto))]
        public async Task<IActionResult> AddAsync([FromBody] SavePagoRequestDto dto)
        {
            try
            {
                var userId = User.FindFirst("UId")!.Value;

                if(dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente para realizar el pago");
                }

                var map = _mapper.Map<CreatePagoRequesDto>(dto);
                map.UsuarioId = userId;     
                var entity = await service.AddAsync(map);

                if(entity!.HasError)
                {
                    return BadRequest(entity.Errors);
                }

                return Created("",entity);
                
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);    
       
            }
        }




        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PagoResponseDto>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
             
                var entities = await service.GetlAllAsync();

                if(entities == null || entities.Count <= 0)
                {

                    return NotFound("No se encontraron Pagos registrado");
                   
                }

                return Ok(entities);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }






        [HttpGet("get-all-by-PedidoId/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PagoResponseDto>))]
        public async Task<IActionResult> GetAllByIdPedido(int id)
        {
            try
            {

                if (id <= 0) 
                {
                    return BadRequest("Debes indicar un pedido para ver el pago");   
               
                }


                var entity = await service.GetBypedidoId(id);

                if (entity == null)
                {

                    return NotFound("No se encontro el Pagos especificado");

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
