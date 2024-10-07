using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian ability-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="options"></param>
public class LoomianAbilitiesService(HttpClient httpClient, IOptions<ApiSettings> options) : ILoomianAbilitiesService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<LoomianAbility>> GetAllAbilitiesAsync()
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/loomianabilities");
        response.EnsureSuccessStatusCode();
        var abilities = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianAbility>>();
        if (abilities is null)
        {
            throw new InvalidOperationException("Failed to deserialize the abilities.");
        }
        return abilities;
    }

    /// <inheritdoc/>
    public async Task<LoomianAbility> GetByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/loomianabilities/{id}");
        response.EnsureSuccessStatusCode();
        var ability = await response.Content.ReadFromJsonAsync<LoomianAbility>();
        if (ability is null)
        {
            throw new InvalidOperationException("Failed to deserialize the ability.");
        }
        return ability;
    }

    /// <inheritdoc/>
    public async Task<LoomianAbility> GetByNameAsync(string name)
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/loomianabilities/name/{name}");
        response.EnsureSuccessStatusCode();
        var ability = await response.Content.ReadFromJsonAsync<LoomianAbility>();
        if (ability is null)
        {
            throw new InvalidOperationException("Failed to deserialize the ability.");
        }
        return ability;
    }

    /// <inheritdoc/>
    public async Task<LoomianAbility> CreateAsync(LoomianAbility ability)
    {
        var response = await httpClient.PostAsJsonAsync($"{options.Value.BaseUrl}/loomianabilities", ability);
        response.EnsureSuccessStatusCode();
        var createdAbility = await response.Content.ReadFromJsonAsync<LoomianAbility>();
        if (createdAbility is null)
        {
            throw new InvalidOperationException("Failed to deserialize the created ability.");
        }
        return createdAbility;
    }
}