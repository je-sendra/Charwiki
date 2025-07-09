using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Models;
using Charwiki.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for authentication-related endpoints.
/// </summary>
/// <param name="authService"></param>
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Endpoint to register a new user.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        OperationResult registerResult = await authService.RegisterUserAsync(userRegisterDto);
        if (registerResult.HasFailed)
        {
            return BadRequest(registerResult.UserMessage);
        }

        return Ok(registerResult.UserMessage);
    }

    /// <summary>
    /// Endpoint to log in a user.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Ensure the login is valid
        OperationResultWithReturnData<User?> loginResult = await authService.ValidateLogin(userLoginDto);

        // If the login failed, return unauthorized
        if (loginResult.HasFailed) return Unauthorized(loginResult.UserMessage);

        if (loginResult.ReturnData == null)
        {
            return Unauthorized("Invalid username or password");
        }

        // Generate JWT token for the user
        string token = authService.GenerateJwtToken(loginResult.ReturnData);

        // Create the response DTO
        UserLoginResponseDto responseDto = new()
        {
            UserId = loginResult.ReturnData.Id,
            Username = loginResult.ReturnData.Username,
            Roles = loginResult.ReturnData.Roles,
            Token = token
        };

        // Return OK with the token
        return Ok(responseDto);
    }
}