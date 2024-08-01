using Client.Classes;
using Client.Interfaces;
using Client.Services;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
}); 

IConfiguration AppConfiguration = builder.Configuration;
var configApp = AppConfiguration.GetSection("Configuration").Get<ConfigApp>();

// Add Configuration
builder.Services.Configure<ConfigApp>(AppConfiguration.GetSection("Configuration"));
builder.Services.AddSingleton(s => s.GetRequiredService<IOptions<ConfigApp>>().Value);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient<IApiService, ApiService>(configApp.Api.Client)
    .ConfigureHttpClient(httpClient =>
    {
        // Configure Client
        httpClient.BaseAddress = new Uri(configApp.Api.Url);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Add("User-Agent", configApp.Api.Client);
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", configApp.Api.Key);
        httpClient.Timeout = TimeSpan.FromSeconds(30);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        // Build Handler
        return new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
        };
    });

// Add Api Services
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddScoped<IMetasService, MetasService>();
builder.Services.AddScoped<ITareasService, TareasService>();

// Add Nugget Services
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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
