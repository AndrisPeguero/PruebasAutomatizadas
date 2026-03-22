

using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReservaBook.Core.Aplication.Dtos.email;
using ReservaBook.Core.Aplication.Dtos.User;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Settings;
using ReservaBook.Infraestructure.Indentity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace ReservaBook.Infraestructure.Indentity.Services
{
    public class AccountServiceForWebApi : IAccountServiceForWebApi
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IEmailService emailService;
        private readonly JwtSettings _jwtSettings;




        public AccountServiceForWebApi(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IEmailService _emailService, IOptions<JwtSettings> JwtSettings)
        {
            emailService = _emailService;
            userManager = _userManager;
            signInManager = _signInManager;
            _jwtSettings = JwtSettings.Value;

        }



        public async Task<LoginResponseDto> Authenticate(LoginDto dto)
        {

            var responseDto = new LoginResponseDto() { Name = "", LastName = "", Errors = [], AccessToken = "", Rol = ""};



            if (string.IsNullOrWhiteSpace(dto.Password))
            {

                responseDto.HasError = true;
                responseDto.Errors!.Add($"You should put the password");
                return responseDto;
            }




            if (string.IsNullOrWhiteSpace(dto.UserName))
            {

                responseDto.HasError = true;
                responseDto.Errors!.Add($"You should put the UserName");
                return responseDto;
            }



            var user = await userManager.FindByNameAsync(dto.UserName);

            if (user == null)
            {
                responseDto.HasError = true;
                responseDto.Errors!.Add($"There is not account registered with this userName: {dto.UserName}");
                return responseDto;

            }




            if (!user!.EmailConfirmed)
            {
                responseDto.HasError = true;
                responseDto.Errors!.Add($"This account {dto.UserName} is not active, you shoul check your email");
                return responseDto;

            }



            var result = await signInManager.PasswordSignInAsync(user.UserName ?? "", dto.Password, false, true);


            if (!result.Succeeded)
            {

                responseDto.HasError = true;
                responseDto.Errors!.Add($"These credentials are invalid for this user: {user.UserName}");
                return responseDto;

            }


            JwtSecurityToken jwtSecurityToken = await GenerateJwtToken(user);



            var rolesList = await userManager.GetRolesAsync(user);


            responseDto.Name = user.Name;
            responseDto.LastName = user.LastName;
            responseDto.Rol = rolesList.FirstOrDefault()!;
            responseDto.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            

            return responseDto;


        }





        public async Task<RegisterResponseDto?> RegisterUser(SaveUserDto? saveUser)
        {

            var response = new RegisterResponseDto() { Name = "", Email = "", Id = "", LastName = "", UserName = "", Errors = [], Message = "" };


            if (saveUser == null)
            {
                return null;

            }


            if (string.IsNullOrWhiteSpace(saveUser.Email)
             || string.IsNullOrWhiteSpace(saveUser.Password)
             || string.IsNullOrWhiteSpace(saveUser.UserName)
             || string.IsNullOrWhiteSpace(saveUser.LastName)
             || string.IsNullOrWhiteSpace(saveUser.Name)
             || string.IsNullOrWhiteSpace(saveUser.Phone)  
             || string.IsNullOrWhiteSpace(saveUser.Role))
            {
                return null;
            }


            var userWithSomeUserName = await userManager.FindByNameAsync(saveUser.UserName);
            if (userWithSomeUserName != null)
            {
                response.HasError = true;
                response.Errors!.Add($"this userName: {saveUser.UserName} is already taken.");
                return response;
            }

            var userWithSomeEmail = await userManager.FindByEmailAsync(saveUser.Email);
            if (userWithSomeEmail != null)
            {
                response.HasError = true;
                response.Errors!.Add($"This email: {saveUser.Email} is already taken.");
                return response;

            }


            AppUser User = new AppUser()
            {

                Name = saveUser.Name,
                LastName = saveUser.LastName,
                Email = saveUser.Email,
                PhoneNumber = saveUser.Phone,
                UserName = saveUser.UserName,
                EmailConfirmed = false,
                ProfileImage = saveUser.ProfileImage ?? "",
                RNC = saveUser.RNC ?? ""


            };


            var result = await userManager.CreateAsync(User, saveUser.Password);

            if (result.Succeeded)
            {
               


                await userManager.AddToRoleAsync(User, saveUser.Role);
                string token = await GetVerificationEmailToken(User);

                var confirmLink =
                   $"http://localhost:5500/Assets/view/confirmAccount.html?userId={User.Id}&token={token}";


                var templatePath = Path.Combine(
                       AppContext.BaseDirectory,"wwwroot",
                      "EmailTemplates",
                      "ConfirmYourAccount.html");


                var html = await File.ReadAllTextAsync(templatePath);

                html = html.Replace("{{USERNAME}}", saveUser.Name);
                html = html.Replace("{{CONFIRM_LINK}}", confirmLink);


                await emailService.SendAsync(new EmailRequestDto()
                {
                    To = saveUser.Email,
                    Subject = "Confirmacion de cuenta",
                    HtmlBody =  html

                });



                var CurrentrolesList = await userManager.GetRolesAsync(User);

                response.Id = User.Id;
                response.Name = User.Name;
                response.UserName = User.UserName ?? "";
                response.LastName = User.LastName;
                response.Email = User.Email ?? "";
                response.IsVerified = User.EmailConfirmed;
                response.Roles = CurrentrolesList.ToList();
                response.Message = "Please Check your email, for verification your account";

            }
            else
            {

                response.HasError = true;
                response.Errors!.AddRange(result.Errors.Select(s => s.Description).ToList());
                return response;

            }


            return response;
        }





        public async Task<EditResponseDto?> EditUser(SaveUserDto? saveUser, bool? IsCreated = false)
        {


            bool IsNotCreated = IsCreated ?? false;
            var response = new EditResponseDto() { Name = "", Email = "", Id = "", LastName = "", UserName = "", Errors = [] };



            if (saveUser == null)
            {
                return null;

            }


            var userWithSomeUserName = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == saveUser.UserName && u.Id != saveUser.Id);
            if (userWithSomeUserName != null)
            {
                response.HasError = true;
                response.Errors!.Add($"this userName: {saveUser.UserName} is already taken.");
                return response;
            }

            var userWithSomeEmail = await userManager.Users.FirstOrDefaultAsync(u => u.Email == saveUser.Email && u.Id != saveUser.Id);
            if (userWithSomeEmail != null)
            {
                response.HasError = true;
                response.Errors!.Add($"This email: {saveUser.Email} is already taken.");
                return response;

            }


            var user = await userManager.FindByIdAsync(saveUser.Id);
            if (user == null)
            {
                response.HasError = true;
                response.Errors!.Add("There is not account registered with this user");
                return response;
            }



            user!.Name = !string.IsNullOrWhiteSpace(saveUser.Name) ? saveUser.Name : user.Name;
            user.LastName = !string.IsNullOrWhiteSpace(saveUser.LastName) ? saveUser.LastName : user.LastName;
            user.PhoneNumber = !string.IsNullOrWhiteSpace(saveUser.Phone) ? saveUser.Phone : user.PhoneNumber;
            user.UserName = !string.IsNullOrWhiteSpace(saveUser.UserName) ? saveUser.UserName : user.UserName;
            user.ProfileImage = string.IsNullOrWhiteSpace(saveUser.ProfileImage) ? user.ProfileImage : saveUser.ProfileImage;
            user.RNC = saveUser.RNC ?? "";
            if (!IsCreated!.Value)
            {
                user.EmailConfirmed = user.Email == saveUser.Email;
            }
            user.Email = !string.IsNullOrWhiteSpace(saveUser.Email) ? saveUser.Email : user.Email;





            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var rolesList = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, rolesList);
                await userManager.AddToRoleAsync(user, saveUser.Role);

                if (!user.EmailConfirmed && !IsNotCreated)
                {


                    string token = await GetVerificationEmailToken(user);

                    var confirmLink =
                       $"http://localhost:5500/Assets/view/confirmAccount.html?userId={user.Id}&token={token}";


                    var templatePath = Path.Combine(
                           AppContext.BaseDirectory, "wwwroot",
                          "EmailTemplates",
                          "ConfirmYourAccount.html");


                    var html = await File.ReadAllTextAsync(templatePath);

                    html = html.Replace("{{USERNAME}}", saveUser.Name);
                    html = html.Replace("{{CONFIRM_LINK}}", confirmLink);


                    await emailService.SendAsync(new EmailRequestDto()
                    {
                        To = saveUser.Email,
                        Subject = "Confirmacion de cuenta",
                        HtmlBody = html

                    });

                }



                if (!string.IsNullOrWhiteSpace(saveUser.Password) && !IsNotCreated)
                {

                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resultChange = await userManager.ResetPasswordAsync(user, token, saveUser.Password);

                    if (resultChange != null && !resultChange.Succeeded)
                    {

                        response.HasError = true;
                        response.Errors.AddRange(resultChange.Errors.Select(s => s.Description).ToList());
                        return response;

                    }

                }


                var CurrentrolesList = await userManager.GetRolesAsync(user);

                response.Id = user.Id;
                response.Name = user.Name;
                response.UserName = user.UserName ?? "";
                response.LastName = user.LastName;
                response.Email = user.Email ?? "";
                response.IsVerified = user.EmailConfirmed;
                response.Roles = CurrentrolesList.ToList();

                return response;
            }
            else
            {

                response.HasError = true;
                response.Errors!.AddRange(result.Errors.Select(s => s.Description).ToList());
                return response;

            }


        }




        public async Task<UserResponseDto> DeleteAsync(string id)
        {
            var response = new UserResponseDto() { Errors = [], HasError = false };

            if (string.IsNullOrEmpty(id))
            {
                response.HasError = true;
                response.Errors.Add("You should put the user id");
                return response;


            }


            var user = await userManager.FindByIdAsync(id);


            if (user == null)
            {

                response.HasError = true;
                response.Errors!.Add($"There is not Account registered with this user");
                return response;

            }


            await userManager.DeleteAsync(user!);

            return response;
        }



        public async Task<List<UserDto>> GetAllUser(bool? IsActive = true)
        {

            var users = userManager.Users;


            if (users == null)
            {
                return new List<UserDto>();
            }


            List<UserDto> ListUserDto = [];
            if (IsActive!.Value && IsActive != null)
            {
                users = users.Where(u => u.EmailConfirmed);

            }
            else
            {
                users = users.Where(u => !u.EmailConfirmed);
            }


            var ListUser = await users.ToListAsync();

            foreach (var item in ListUser)
            {

                var rolesList = await userManager.GetRolesAsync(item);


                ListUserDto.Add(new UserDto()
                {

                    Id = item.Id,
                    Name = item.Name,
                    UserName = item.UserName ?? "",
                    LastName = item.LastName,
                    Email = item.Email ?? "",
                    IsVerified = item.EmailConfirmed,
                    Phone = item.PhoneNumber ?? "",
                    ProfileImage = item.ProfileImage,
                    Role = rolesList.FirstOrDefault() ?? ""

                });



            }

            return ListUserDto;


        }



        public async Task<UserDto?> GetUserByEmail(string gmail)
        {

            var user = await userManager.FindByEmailAsync(gmail);


            if (user == null)
            {
                return null;

            }


            var rolesList = await userManager.GetRolesAsync(user);

            var userDto = new UserDto()
            {

                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                Phone = user.PhoneNumber ?? "",
                ProfileImage = user.ProfileImage,
                IsVerified = user.EmailConfirmed,
                Role = rolesList.FirstOrDefault() ?? ""


            };


            return userDto;
        }



        public async Task<UserDto?> GetUserById(string id)
        {

            var user = await userManager.FindByIdAsync(id);


            if (user == null)
            {
                return null;

            }


            var rolesList = await userManager.GetRolesAsync(user);

            var userDto = new UserDto()
            {

                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                Phone = user.PhoneNumber ?? "",
                ProfileImage = user.ProfileImage,
                IsVerified = user.EmailConfirmed,
                Role = rolesList.FirstOrDefault() ?? ""


            };


            return userDto;
        }




        public async Task<UserDto?> GetUserByUserName(string userName)
        {

            var user = await userManager.FindByNameAsync(userName);


            if (user == null)
            {
                return null;

            }

            var rolesList = await userManager.GetRolesAsync(user);

            var userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                Phone = user.PhoneNumber ?? "",
                ProfileImage = user.ProfileImage,
                IsVerified = user.EmailConfirmed,
                Role = rolesList.FirstOrDefault() ?? ""

            };


            return userDto;
        }



        public async Task<UserResponseDto?> RessetPassowrd(RessetPasswordRequestDto? request)
        {

            var response = new UserResponseDto() { Errors = [], HasError = false };


            if (request == null)
            {
                return null;
            }


            if (string.IsNullOrWhiteSpace(request.Id)
               || string.IsNullOrWhiteSpace(request.Token)
               || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }




            var user = await userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                response.HasError = true;
                response.Errors!.Add($"there is not account registered with this user");
                return response;

            }


            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Errors!.Add($"An error ocurred while resset password");
                return response;

            }


            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);

            return response;
        }




        public async Task<UserResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {

            var response = new UserResponseDto() { Errors = [], HasError = false };


            if (string.IsNullOrEmpty(request.UserName))
            {
                response.HasError = true;
                response.Errors.Add("You should put de userName");
                return response;

            }


            var user = await userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Errors!.Add($"there is not account registered with this userName {request.UserName}");
                return response;

            }



            var ressetToken = await GetRessetPassworToken(user);
            user.EmailConfirmed = false;


            var result = await userManager.UpdateAsync(user);


            var encodeToken = Uri.EscapeDataString(ressetToken);
            var confirmLink =
               $"http://localhost:5500/Assets/view/changePassword.html?userId={user.Id}&token={encodeToken}";


            var templatePath = Path.Combine(
                   AppContext.BaseDirectory, "wwwroot",
                  "EmailTemplates",
                  "ChangePassword.html");


            var html = await File.ReadAllTextAsync(templatePath);

            html = html.Replace("{{USERNAME}}", request.UserName);
            html = html.Replace("{{CONFIRM_LINK}}", confirmLink);


            await emailService.SendAsync(new EmailRequestDto()
            {
                To = user.Email!,
                Subject = "Resetear la contraseña",
                HtmlBody = html

            });

            if (result.Succeeded)
            {
                response.Message = "Please Check your email, for reseet your password";
            }

            return response;
        }








        public async Task<ConfirmResponseDto?> confirmAccounAsync(ConfirmRequestDto? dto)
        {
            var response = new ConfirmResponseDto() { HasError = false, Message = "" };

            if (dto == null)
            {
                return null;
            }


            if (string.IsNullOrEmpty(dto.UserId)
                || string.IsNullOrEmpty(dto.Token)
               ) 
            {

                return null;
           
            }


            var user = await userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {

                response.HasError = true;
                response.Message = "There is not account registered with this user";
                return response;

            }


            dto.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));
            var result = await userManager.ConfirmEmailAsync(user, dto.Token);
            if (result.Succeeded)
            {
                response.HasError = false;
                response.Message = $"Account confirmed for {user.Email}. you can now use the app";
                return response;

            }
            else
            {

                response.HasError = true;
                response.Message = $"An error ocurred when while confirming this email {user.Email}, Please verificate your token";
                return response;

            }


        }











        #region private Method
        private async Task<string> GetVerificationEmailToken(AppUser user)
        {

            string Token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Token));
            
            return Token;

        }


        private async Task<string> GetRessetPassworToken(AppUser user)
        {

            string Token = await userManager.GeneratePasswordResetTokenAsync(user);
            Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Token));   
            return Token;

        }



        public async Task<JwtSecurityToken> GenerateJwtToken(AppUser user)
        {
        
        
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);



            var rolesClaims = new List<Claim>();

            foreach(var role in roles)
            {
                rolesClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            var claims = new[]
            {

                new Claim(JwtRegisteredClaimNames.Sub,user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email ?? ""),
                new Claim("UId",user.Id),

            }.Union(userClaims).Union(rolesClaims);


            var symmectriSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signinCredentials = new SigningCredentials(symmectriSecurityKey,SecurityAlgorithms.HmacSha256);


            var JwtSecuritytoken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signinCredentials);


            return JwtSecuritytoken;

        }
        #endregion




    }


}
