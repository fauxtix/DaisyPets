

using DaisyPets.Infrastructure.Context;
using DaisyPets.WebApi.Configuration;
using DaisyPets.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<ApiKeyMiddleware>();

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
