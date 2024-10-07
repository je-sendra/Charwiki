using System.Text;
using System.Text.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for game version info-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="options"></param>
public class GameVersionInfosService(HttpClient httpClient, IOptions<ApiSettings> options) : IGameVersionInfosService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<GameVersionInfo>> GetAllGameVersionInfosAsync()
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/gameversioninfos");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var gameVersionInfos = JsonSerializer.Deserialize<IEnumerable<GameVersionInfo>>(content);
        if (gameVersionInfos is null)
        {
            throw new InvalidOperationException("Failed to deserialize the game version infos.");
        }
        return gameVersionInfos;
    }

    /// <inheritdoc/>
    public async Task<GameVersionInfo> GetByIdAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/gameversioninfos/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var gameVersionInfo = JsonSerializer.Deserialize<GameVersionInfo>(content);
        if (gameVersionInfo is null)
        {
            throw new InvalidOperationException("Failed to deserialize the game version info.");
        }
        return gameVersionInfo;
    }

    /// <inheritdoc/>
    public async Task<GameVersionInfo> CreateGameVersionInfoAsync(GameVersionInfo gameVersionInfo)
    {
        var json = JsonSerializer.Serialize(gameVersionInfo);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{options.Value.BaseUrl}/gameversioninfos", content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdGameVersionInfo = JsonSerializer.Deserialize<GameVersionInfo>(responseContent);
        if (createdGameVersionInfo is null)
        {
            throw new InvalidOperationException("Failed to deserialize the created game version info.");
        }
        return createdGameVersionInfo;
    }
}