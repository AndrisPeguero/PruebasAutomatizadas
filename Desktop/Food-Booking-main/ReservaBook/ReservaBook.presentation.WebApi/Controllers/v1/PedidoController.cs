using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize( Roles = "Cliente")]
    public class PedidoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPedidoService pedidoService;




        public PedidoController(IMapper _mapper, IPedidoService pedidoService)
        {

            this._mapper = _mapper;
            this.pedidoService = pedidoService; 


        }



        [HttpGet("get-All-pedidos")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PedidoResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPedidos()
        {
            try
            {
                var entity = await pedidoService.GetlAllAsync();

                if (entity == null)
                {
                   
                    return NotFound("No se encontraron pedidos registrado, favor verificar");
                
                }


                return Ok(entity);  


            }
            catch (Exception ex)
            { 
                
               return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);       
            
            
            }
            
         
        }



        [HttpPost("add-pedido")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPedido([FromBody] SavePedidoRequestDto dto)
        {
            try
            {

                var userId = User.FindFirst("UId")!.Value;

                if(dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente  favor verificar");
                }


                var map = _mapper.Map<CreatePedidoRequestDto>(dto);
                map.ClienteId = userId;
                var entity = await pedidoService.AddAsync(map);


                if (entity == null && entity!.HasError)
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





        [HttpPut("update-pedido/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePedido(int id,[FromBody] UpdatePedidoRequestDto dto)
        {
            try
            {

                var userId = User.FindFirst("UId")!.Value;

                if (dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente, favor verificar");
                }



                var map = _mapper.Map<CreatePedidoRequestDto>(dto);
                map.ClienteId = userId;
                var entity = await pedidoService.UpdateAsync(id,map);



                if (entity == null && entity!.HasError)
                {
                    return BadRequest(entity.Errors);
                }



                return Ok(entity);


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }


        }



        [HttpDelete("delete-pedido/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePedido([FromRoute] PedidoIdRequestDto dto)
        {
            try
            {


                if (dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente, favor verificar");
                }

      
                var entity = await pedidoService.DeleteAsync(dto.Id);



                if (!entity)
                {
                    return NotFound("No se encontro el pedido especificado, favor verificar");
                }



                return NoContent();


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }


        }




        [HttpGet("getById-pedido/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdPedido([FromRoute] PedidoIdRequestDto dto)
        {
            try
            {

                if (dto == null)
                {
                    return BadRequest("Debes introduccir los valores correctamente, favor verificar");
                }


                var entity = await pedidoService.GetByIdAsync(dto.Id);



                if (entity == null)
                {
                    return NotFound("No se pudo encontrar el pedido indicado, favor verificar");
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
