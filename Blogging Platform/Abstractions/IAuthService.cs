using Blogging_Platform.DTOs.UserDTOs;

namespace Blogging_Platform.Abstractions
{
    public interface IAuthService
    {
        public Task<RegisterResponse> RegisterAsync(UserRegisterDto dto);
        public Task<LoginResponse> LoginAsync(UserLoginDto dto);
    }
}
