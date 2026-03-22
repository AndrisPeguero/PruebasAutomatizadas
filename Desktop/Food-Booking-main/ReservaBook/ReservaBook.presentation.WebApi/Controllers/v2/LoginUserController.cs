using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.User;
using ReservaBook.Core.Aplication.Interfaces;


namespace ReservaBook.presentation.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class LoginUserController : BaseApiController
    {

        private readonly IAccountServiceForWebApi _accountService;



        public LoginUserController(IAccountServiceForWebApi _accountService)
        {

            this._accountService = _accountService;

        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            try
            {
                return Ok(await _accountService.Authenticate(dto));

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


    }
}
