namespace Blogging_Platform.DTOs.UserDTOs
{
    public record LoginResponse(bool Flag, string Message = null!, string Jwt = null!);
}
