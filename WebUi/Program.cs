using Blazored.LocalStorage;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Services;
using Charwiki.WebUi.Components;
using Charwiki.WebUi.Services;

namespace Charwiki.WebUi;

/// <summary>
/// The main program class.
/// </summary>
public static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Add API settings.
        builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

        // Add HttpClient.
        builder.Services.AddHttpClient();

        // Add Local Storage service.
        builder.Services.AddBlazoredLocalStorage();

        // Add User Token service.
        builder.Services.AddScoped<UserTokenService>();

        // Register Charwiki custom services for development.
        RegisterCharwikiApiServices(builder.Services);

        // Register Blazor Bootstrap.
        builder.Services.AddBlazorBootstrap();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        UpdateCachedDataLists(builder.Services).GetAwaiter().GetResult();

        app.Run();
    }

    private static void RegisterCharwikiApiServices(IServiceCollection services)
    {
        services.AddScoped<ILoomiansService, LoomiansService>();
        services.AddScoped<ILoomianSetsService, LoomianSetsService>();
        services.AddScoped<ILoomianMovesService, LoomianMovesService>();
        services.AddScoped<ILoomianAbilitiesService, LoomianAbilitiesService>();
        services.AddScoped<ILoomianItemsService, LoomianItemsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameVersionInfosService, GameVersionInfosService>();
        services.AddScoped<ITagsService, TagsService>();
    }

    private static void RegisterCharwikiMockServices(IServiceCollection services)
    {
    }

    internal static async Task UpdateCachedDataLists(IServiceCollection services)
    {
        ILoomiansService loomiansService = services.BuildServiceProvider().GetRequiredService<ILoomiansService>();
        CachedDataLists.Loomians = await loomiansService.GetAllAsync() ?? [];

        ILoomianAbilitiesService loomianAbilitiesService = services.BuildServiceProvider().GetRequiredService<ILoomianAbilitiesService>();
        CachedDataLists.Abilities = await loomianAbilitiesService.GetAllAsync() ?? [];

        ILoomianItemsService loomianItemsService = services.BuildServiceProvider().GetRequiredService<ILoomianItemsService>();
        CachedDataLists.Items = await loomianItemsService.GetAllAsync() ?? [];

        ILoomianMovesService loomianMovesService = services.BuildServiceProvider().GetRequiredService<ILoomianMovesService>();
        CachedDataLists.Moves = await loomianMovesService.GetAllAsync() ?? [];

        IGameVersionInfosService gameVersionInfosService = services.BuildServiceProvider().GetRequiredService<IGameVersionInfosService>();
        CachedDataLists.GameVersionInfos = await gameVersionInfosService.GetAllAsync() ?? [];

        ITagsService tagsService = services.BuildServiceProvider().GetRequiredService<ITagsService>();
        CachedDataLists.Tags = await tagsService.GetAllAsync() ?? [];
    }
}
