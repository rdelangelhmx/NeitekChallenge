using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Server.Classes;
using Server.Features;
using Server.Helpers;
using Server.Interfaces;
using Server.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IConfiguration AppConfiguration = builder.Configuration;
var configApp = AppConfiguration.GetSection("Configuration").Get<ConfigApp>();

// Add Configuration
builder.Services.Configure<ConfigApp>(AppConfiguration.GetSection("Configuration"));
builder.Services.AddSingleton(s => s.GetRequiredService<IOptions<ConfigApp>>().Value);

builder.Services.AddDbContext<NeitekContext>(options => options.UseSqlServer(configApp.DataBase.Conn, options => {
    options.CommandTimeout(30);
    options.EnableRetryOnFailure(3);
}));
builder.Services.AddDbContext<NeitekContext>();
builder.Services.AddScoped<IMetasRepository, MetasRepository>();
builder.Services.AddScoped<ITareasRepository, TareasRepository>();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient();
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc($"v{configApp.Application.Version}", new OpenApiInfo
    {
        Title = configApp.Application.Name,
        Version = $"v{configApp.Application.Version}",
        Description = configApp.Application.Description,
        Contact = new OpenApiContact
        {
            Name = configApp.Application.Company,
            Email = configApp.Application.Email,
            Url = new Uri(configApp.Application.WebPage),
        }
    });
    options.CustomSchemaIds(type => type.ToString());
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    options.AddSecurityDefinition(configApp.Application.Scheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Description = "Authentication Token",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = configApp.Application.Scheme }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(configApp.Application.Scheme)
    .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>(configApp.Application.Scheme, null);

var app = builder.Build();

app.UseCors(opt => opt.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"v{configApp.Application.Version}/swagger.json", $"{configApp.Application.Name} v{configApp.Application.Version}");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
