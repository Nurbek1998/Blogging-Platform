using Blogging_Platform.Abstractions;
using Blogging_Platform.Data;
using Blogging_Platform.DTOs.UserDTOs;
using Blogging_Platform.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Blogging_Platform.Validators;

namespace Blogging_Platform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var validator = new RegistrationValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }
        //if (!ModelState.IsValid)
        //    return BadRequest(ModelState);
       
        var response = await _service.RegisterAsync(dto);

        if (response.Flag is true)
        {
            return Created("Created successfully", response);
        }

        else
            return BadRequest("The registration failed suddenly");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var response = await _service.LoginAsync(dto);

        if (response.Jwt is null)
            return Unauthorized("Invalid username or password");

        return Ok(new { Token = response.Jwt });
    }
}
