using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservaBook.Core.Aplication.Dtos.User;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.presentation.WebApi.Handlers;
using System.Security.Claims;

namespace ReservaBook.presentation.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [Authorize] 
    public class ManagerAccountController : Controller
    {

        private readonly IAccountServiceForWebApi _accountService;
        private readonly IMapper _mapper;



        public ManagerAccountController(IAccountServiceForWebApi _accountService, IMapper _mapper)
        {

            this._accountService = _accountService;
            this._mapper = _mapper;

        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {


                var users = await _accountService.GetAllUser();

                if (users == null) 
                {
                    return Ok(new List<UserDto>());
                }

                return Ok(users);


            }
            catch (Exception ex)
            {
                 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            
     
            }
        
       
        }




    
        [HttpPost("EditUser")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditResponseDto))]
        public async Task<IActionResult> EditUser([FromForm] EditUserRequestMinDto dto)
        {

            try
            {
                var UserId = User.FindFirst("UId")?.Value;
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                var result = await _accountService.EditUser(
                    new SaveUserDto()
                    { Id = UserId,
                        Name = dto.Name,
                        LastName = dto.LastName,
                        Email = dto.Email ?? "",
                        Password = dto.Password ?? "",
                        ProfileImage = FileHandler.Upload(dto.ProfileImage, UserId!, null!, true, ""),
                        Phone = dto.Phone,
                        UserName = dto.UserName ?? "",
                        Role = role
                    });


                if (result == null || result.HasError)
                {
                    return BadRequest(result!.Errors);
                }

              
                return Created("", result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var userDeleted = await _accountService.DeleteAsync(id);

                if (userDeleted.HasError)
                {
                      return NotFound(userDeleted.Errors);  
                }
                
                return NoContent();

            }
            catch (Exception ex)
            {
            

                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message); 
            
            }
       
        }


























    }
}
