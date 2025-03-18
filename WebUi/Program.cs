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

        // Add JWT token storing service.
        builder.Services.AddScoped<LoginStateService>();

        // Add JWT HTTP message handler and its HTPP client.
        builder.Services.AddTransient<JwtHttpMessageHandler>();
        builder.Services.AddHttpClient("ApiClient")
            .AddHttpMessageHandler<JwtHttpMessageHandler>();

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

        app.Run();
    }

    private static void RegisterCharwikiApiServices(IServiceCollection services)
    {
        RegisterApiServiceWithHttpClient<ILoomiansService, LoomiansService>(services);
        RegisterApiServiceWithHttpClient<ILoomianSetsService, LoomianSetsService>(services);
        RegisterApiServiceWithHttpClient<ILoomianMovesService, LoomianMovesService>(services);
        RegisterApiServiceWithHttpClient<ILoomianAbilitiesService, LoomianAbilitiesService>(services);
        RegisterApiServiceWithHttpClient<ILoomianItemsService, LoomianItemsService>(services);
        RegisterApiServiceWithHttpClient<IUserService, UserService>(services);   
    }

    private static void RegisterCharwikiMockServices(IServiceCollection services)
    {
        services.AddScoped<ILoomiansService, MockLoomiansService>();
        services.AddScoped<ILoomianSetsService, MockLoomianSetsService>();
        services.AddScoped<ILoomianMovesService, MockLoomianMovesService>();
    }

    private static void RegisterApiServiceWithHttpClient<T, Y>(IServiceCollection services)
    where T : class  // T should be the interface
    where Y : class, T  // Y should be a class and implement T (the interface)
    {
        services.AddScoped<T>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("ApiClient");

            // Use ActivatorUtilities to create an instance of Y, passing the HttpClient
            return ActivatorUtilities.CreateInstance<Y>(sp, httpClient) as T;
        });
    }
}
