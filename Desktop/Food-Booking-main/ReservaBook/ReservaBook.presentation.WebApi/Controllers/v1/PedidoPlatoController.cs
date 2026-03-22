using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.pdidoPlato;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Cliente")]
    public class PedidoPlatoController : BaseApiController
    {
        private readonly IPedidoPlatoService pedidoPlatoService;
        private readonly IMapper mapper;


        public PedidoPlatoController(IPedidoPlatoService pedidoPlatoService, IMapper mapper)
        {
           this.pedidoPlatoService = pedidoPlatoService;
           this.mapper = mapper;
        
        }




        [HttpGet("get-all-platos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var entities = await pedidoPlatoService.GetlAllAsync(); 


                if (entities.Count <= 0 || !entities.Any())
                {
                    return NotFound("No se encontraron platos agregados");
                }


                return Created("", entities);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);



            }
        }











        [HttpPost("Add-platos-all-pedido")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] List<SavePedidoPlatoRequestDto>? dto)
        {
            try
            {

                if(dto.Count <= 0 || !dto.Any())
                {
                    return BadRequest("Debes indicar almenos un plato para agregar");
                }


                var map = mapper.Map<List<CreatePedidoPlatoDto>>(dto);
                var listEntities = await pedidoPlatoService.AddRangeAsync(map);


                if (listEntities.Count <= 0 || !listEntities.Any())
                {
                    foreach (var entity in listEntities)
                    {

                        if (entity.HasError)
                        {

                            return BadRequest(entity.Errors);

                        }
                    }
                }

                return Created("",listEntities);

            }
            catch (Exception ex)
            {
            
                
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            
            
  
            }
        }






        [HttpDelete("delete-plato-del-menu/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeletePedidoPlatoRequestDto dto)
        {
            try
            {

                if (dto.Id <= 0 || dto == null)
                {
                    return BadRequest("Debes indicar almenos un plato para eliminar");
                }


                var pedido = await pedidoPlatoService.DeleteAsync(dto.Id);
                
                if(!pedido)
                {

                    return NotFound("No se contro el plato especificado");
                }


                return NoContent();

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);



            }
        }





        [HttpPut("update-plato-pedido/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody] UpdatePedidoPlatoRequestDto dto)
        {
            try
            {

                if (id <= 0)
                {
                    return BadRequest("Debes indicar  un plato para actualizar");
                }

                if(dto == null)
                {

                    return BadRequest("Debes introduccir valores valido para actualizar");

                }

                var map = mapper.Map<CreatePedidoPlatoDto>(dto);
                var entity = await pedidoPlatoService.UpdateAsync(id,map);

                if (entity == null || entity.HasError)
                {

                    return NotFound(entity!.Errors);
                
                }


                return Created("", entity);

            }
            catch (Exception ex)
            {


                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);



            }
        }












    }
}
