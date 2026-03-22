using Microsoft.AspNetCore.Mvc;

namespace ReservaBook.presentation.WebApi.Controllers
{
    [Route("Api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
     
        


    }
}



