using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Models.OperationResult;
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
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await authService.RegisterUserAsync(userRegisterDto);

        return Ok();
    }

    /// <summary>
    /// Endpoint to log in a user.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
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

        // Return OK with the token
        return Ok(token);
    }
}