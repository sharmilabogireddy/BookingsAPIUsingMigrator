using BookingsAPIUsingMigrator.api.Extensions;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Needed to load config from file mounts in Pod:
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
config
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true)
    .AddJsonFile($"/etc/appsettings/appsettings.{env}.json", true, true);

// Add services to the container.

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
};

builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.ConfigureRepositoryManager(config);

builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureMediatR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Cors enabled
builder.Services.AddCors(options =>
{
    options.AddPolicy("GLOBAL_CORS_POLICY",
        builder => builder.SetIsOriginAllowed(origin => true)
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

var pathbase = config.GetSection("Path")["Base"];
app.UsePathBase(pathbase);
// Configure the HTTP request pipeline.
if (!app.Environment.EnvironmentName.Equals("Production"))
{
    app.UseSwagger();

    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint($"{pathbase}/swagger/v1/swagger.json", "API v1.0");
        
    });

    if (app.Environment.EnvironmentName.Equals("Local"))
        app.UseHttpsRedirection();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
