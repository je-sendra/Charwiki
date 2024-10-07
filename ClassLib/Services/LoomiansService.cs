using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Models;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="options"></param>
public class LoomiansService(HttpClient httpClient, IOptions<ApiSettings> options) : ILoomiansService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<Loomian>> GetAllLoomiansAsync()
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/loomians");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var loomians = JsonSerializer.Deserialize<IEnumerable<Loomian>>(content);
        if (loomians is null)
        {
            throw new InvalidOperationException("Failed to deserialize the loomians.");
        }
        return loomians;
    }

    /// <inheritdoc/>
    public async Task<Loomian> GetByIdAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/loomians/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var loomian = JsonSerializer.Deserialize<Loomian>(content);
        if (loomian is null)
        {
            throw new InvalidOperationException("Failed to deserialize the loomian.");
        }
        return loomian;
    }

    /// <inheritdoc/>
    public async Task<Loomian> GetLoomianByNameAsync(string name)
    {
        var response = await httpClient.GetAsync($"{options.Value.BaseUrl}/loomians/{name}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var loomian = JsonSerializer.Deserialize<Loomian>(content);
        if (loomian is null)
        {
            throw new InvalidOperationException("Failed to deserialize the loomian.");
        }
        return loomian;
    }

    /// <inheritdoc/>
    public async Task<Loomian> CreateLoomianAsync(Loomian loomian)
    {
        var response = await httpClient.PostAsJsonAsync($"{options.Value.BaseUrl}/loomians", loomian);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdLoomian = JsonSerializer.Deserialize<Loomian>(responseContent);
        if (createdLoomian is null)
        {
            throw new InvalidOperationException("Failed to deserialize the created loomian.");
        }
        return createdLoomian;
    }
}