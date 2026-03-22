using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Interfaces;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class NotificacionController : BaseApiController
    {
        private readonly INotificacionService service;
        private readonly IMapper _mapper;


        public NotificacionController(INotificacionService service, IMapper _mapper)
        {
             this.service = service;
             this._mapper = _mapper;
        
        }






        [HttpGet("get-notificaciones-usuario")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async  Task<IActionResult> GetByUserId()
        {
            try
            {


                var userId = User.FindFirst("UId")!.Value;


                var entities = await service.GetNotificacionByReceptorId(userId);

                if(entities == null || entities.Count <= 0)
                {
                    return NotFound("No se encontraron notificaciones registradas");
                }


                return Ok(entities);

            }
            catch (Exception ex)
            {


                throw new Exception("Ocurrio un error al intentar obtener tu notificaciones" + ex.Message);
            
     
            }
           
        }



        [HttpDelete("delete-notificaciones/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

             
                var entity = await service.DeleteAsync(id);

                if (!entity)
                {
                    return NotFound("No se encontraron notificaciones registradas");
                }


                return NoContent();

            }
            catch (Exception ex)
            {


                throw new Exception("Ocurrio un error al intentar obtener tu notificaciones" + ex.Message);


            }

        }






    }
}
