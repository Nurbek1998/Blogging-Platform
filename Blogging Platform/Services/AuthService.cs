using Blogging_Platform.Abstractions;
using Blogging_Platform.Data;
using Blogging_Platform.DTOs.UserDTOs;
using Blogging_Platform.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogging_Platform.Services
{
    public class AuthService(AppDbContext _context, IConfiguration configuration, ILogger<AuthService> logger) : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(UserLoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user is null)
                return new LoginResponse(false, "The user is not found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (checkPassword)
                return new LoginResponse(true, "Login successfully!", JwtTokenGenerate(user));

            else return new LoginResponse(false, "Wrong information");
        }

        public async Task<RegisterResponse> RegisterAsync(UserRegisterDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (existingUser is not null)
                return new RegisterResponse(false, "The user is already exist");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role.ToLower()
            };
            logger.LogInformation("Ishlayapti");
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new RegisterResponse(true, "The user is successfully registered!");
        }

        private string JwtTokenGenerate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToLower()),
            };

            var token = new JwtSecurityToken(
                audience: configuration["Jwt:Audience"],
            issuer: configuration["Jwt:Issuer"],
            claims: userClaims,
                expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("Jwt:TokenValidityMins", 30)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
