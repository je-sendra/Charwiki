using System.Runtime.InteropServices;
using Blazored.LocalStorage;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Services;
using Charwiki.WebUi.Services;
using Microsoft.AspNetCore.Components;

namespace Charwiki.WebUi.Components.Pages;

/// <summary>
/// Login page component.
/// </summary>
public partial class Login
{
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private UserTokenService UserTokenService { get; set; } = default!;

    private string Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private string ErrorMessage { get; set; } = string.Empty;

    private string ActiveTab { get; set; } = "Login";

    private async Task HandleLoginAsync()
    {
        try
        {
            UserLoginDto userLoginDto = new()
            {
                Username = Username,
                Password = Password
            };

            string token = await UserService.LoginAsync(userLoginDto);

            if (!string.IsNullOrEmpty(token))
            {
                await UserTokenService.SetAuthTokenAsync(token);

                // Redirect to the home page or another page after successful login
                NavigationManager.NavigateTo("/", forceLoad: true);
            }
            else
            {
                ErrorMessage = "Login failed: Invalid credentials.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Login failed: {ex.Message}";
        }
    }

    private async Task HandleRegisterAsync()
    {
        try
        {
            UserRegisterDto userRegisterDto = new()
            {
                Username = Username,
                Password = Password
            };

            await UserService.RegisterAsync(userRegisterDto);

            await HandleLoginAsync(); // Automatically log in after registration
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Registration failed: {ex.Message}";
        }
    }

    private void SwitchTab(string tab)
    {
        ActiveTab = tab;
        ErrorMessage = string.Empty; // Clear error message when switching tabs
    }
}