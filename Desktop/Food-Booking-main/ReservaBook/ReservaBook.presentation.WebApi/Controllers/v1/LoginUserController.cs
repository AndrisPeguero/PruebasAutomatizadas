using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.User;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.presentation.WebApi.Handlers;
using System.Xml;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LoginUserController : BaseApiController
    {

        private readonly IAccountServiceForWebApi _accountService;
        private readonly IMapper _mapper;



        public LoginUserController(IAccountServiceForWebApi _accountService, IMapper _mapper)
        {

            this._accountService = _accountService;
            this._mapper = _mapper;

        }



        [HttpPost("login")]
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




        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {

            try
            {
               
               var save = _mapper.Map<SaveUserDto>(dto);
               var result = await _accountService.RegisterUser(save);


                if (result == null || result.HasError)
                {
                    return BadRequest(result!.Errors);
                }



                save.ProfileImage =  FileHandler.Upload(dto.ProfileImage,result.Id,"",false,"UserId");
                save.Id = result.Id;
                var resultEdit = await _accountService.EditUser(save,true);

                return Created("",result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
           
            }
        
        }



        [HttpPost("confirm-account")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Confirm([FromBody] ConfirmRequestDto dto)
        {

            try
            {

                var result = await _accountService.confirmAccounAsync(dto);


                if(result == null || result.HasError)
                {
                    return BadRequest(result);
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }





        [HttpPost("get-resset-token")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRessetToken([FromBody] ForgotPasswordRequestDto dto)
        {

            try
            {

                var result = await _accountService.ForgotPasswordAsync(dto);


                if (result.HasError || result == null)
                {
                    return BadRequest(result!);
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }



        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] RessetPasswordRequestDto dto)
        {

            try
            {
                
                var result = await _accountService.RessetPassowrd(dto);


                if (result == null || result.HasError)
                {
                    return BadRequest(result!);
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
