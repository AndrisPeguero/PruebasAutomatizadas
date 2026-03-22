


using ReservaBook.Core.Aplication.Dtos.User;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IAccountServiceForWebApi
    {
        Task<LoginResponseDto> Authenticate(LoginDto dto);
        Task<ConfirmResponseDto?> confirmAccounAsync(ConfirmRequestDto? dto);
        Task<UserResponseDto> DeleteAsync(string id);
        Task<EditResponseDto?> EditUser(SaveUserDto? saveUser, bool? IsCreated = false);
        Task<UserResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task<List<UserDto>> GetAllUser(bool? IsActive = true);
        Task<UserDto?> GetUserByEmail(string gmail);
        Task<UserDto?> GetUserById(string id);
        Task<UserDto?> GetUserByUserName(string userName);
        Task<RegisterResponseDto?> RegisterUser(SaveUserDto? saveUser);
        Task<UserResponseDto?> RessetPassowrd(RessetPasswordRequestDto? request);
      
    }
}