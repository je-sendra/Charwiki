using System.Net.Http.Json;
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
        var gameVersionInfos = await response.Content.ReadFromJsonAsync<IEnumerable<GameVersionInfo>>();
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
        var gameVersionInfo = await response.Content.ReadFromJsonAsync<GameVersionInfo>();
        if (gameVersionInfo is null)
        {
            throw new InvalidOperationException("Failed to deserialize the game version info.");
        }
        return gameVersionInfo;
    }

    /// <inheritdoc/>
    public async Task<GameVersionInfo> CreateGameVersionInfoAsync(GameVersionInfo gameVersionInfo)
    {
        var response = await httpClient.PostAsJsonAsync($"{options.Value.BaseUrl}/gameversioninfos", gameVersionInfo);
        response.EnsureSuccessStatusCode();
        var createdGameVersionInfo = await response.Content.ReadFromJsonAsync<GameVersionInfo>();
        if (createdGameVersionInfo is null)
        {
            throw new InvalidOperationException("Failed to deserialize the created game version info.");
        }
        return createdGameVersionInfo;
    }
}