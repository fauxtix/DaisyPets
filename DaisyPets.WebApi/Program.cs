using DaisyPets.Infrastructure.Context;
using DaisyPets.WebApi.Configuration;
using DaisyPets.WebApi.Helpers;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Set the minimum log level
    .WriteTo.Console()
    .WriteTo.EventLog("Daisy Pets API")
    .CreateLogger();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIyMzE2MkAzMjMxMmUzMDJlMzBCdlcySmlaSjFFeU5BQjNaUDNYQ0R3VHFROCttQ3FiWi9TbStncWVGcGlFPQ ==");

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("lookupTablesPolicy", builder => builder.Expire(TimeSpan.FromHours(2)).Tag("lookupTables"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<ApiKeyMiddleware>();

app.UseOutputCache();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();
}

app.Run();
