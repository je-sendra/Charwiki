using System.Net.Http.Json;
using Microsoft.AspNetCore.JsonPatch;
using VewTech.Charwiki.Library.Interfaces;

namespace VewTech.Charwiki.Library.Helpers;

public class ApiHelper<T>(string endpointRoute) where T : class, IApiModel
{
    /// <summary>
    /// The route where the API endpoint is.
    /// </summary>
    private string EndpointRoute { get; set; } = endpointRoute;

    /// <summary>
    /// This is a wrapper around the GET endpoint. Will return all the resources.
    /// </summary>
    /// <returns>A list with all the resources.</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IEnumerable<T>> GetAll()
    {
        var response = await Statics.ApiClient.GetAsync(EndpointRoute);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<T>>(options: Statics.JsonSerializerOptions) ?? throw new NullReferenceException();
        return content;
    }

    /// <summary>
    /// This is a wrapper around the POST endpoint. Will post the resource object.
    /// </summary>
    /// <returns>The created resource.</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> Post(T resource)
    {
        var response = await Statics.ApiClient.PostAsJsonAsync(EndpointRoute, resource);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<T>(options: Statics.JsonSerializerOptions) ?? throw new NullReferenceException();
        return content;
    }

    /// <summary>
    /// This is a wrapper around the GET "/{id}" endpoint. Will get the resource with the specified id.
    /// </summary>
    /// <param name="id">The id of the resource to retrieve.</param>
    /// <returns>The resource with the specified id.</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> GetById(Guid id)
    {
        var response = await Statics.ApiClient.GetAsync($"{EndpointRoute}/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<T>(options: Statics.JsonSerializerOptions) ?? throw new NullReferenceException();
        return content;
    }

    /// <summary>
    /// This is a wrapper around the PATCH "/{id}" endpoint. It will update the resource with the specified id based on the JsonPatchDocument provided.
    /// </summary>
    /// <param name="id">The id of the object to update.</param>
    /// <param name="patch">The JsonPatchDocument with the updates to perform on the resource.</param>
    /// <returns>The updated resource.</returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<T> Patch(Guid id, JsonPatchDocument<T> patch)
    {
        var response = await Statics.ApiClient.PatchAsJsonAsync
        ($"{EndpointRoute}/{id}", patch.Operations, options: Statics.JsonSerializerOptions);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<T>(options: Statics.JsonSerializerOptions) ?? throw new NullReferenceException();
        return content;
    }

    /// <summary>
    /// This is a wrapper around the DELETE "/{id}" endpoint. It will delete the resource with the given id.
    /// </summary>
    /// <param name="id">The id of the resource to remove.</param>
    /// <returns></returns>
    public async Task Delete(Guid id)
    {
        var response = await Statics.ApiClient.DeleteAsync($"{EndpointRoute}/{id}");
        response.EnsureSuccessStatusCode();
    }
}