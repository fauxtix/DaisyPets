using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DaisyPets.WebApi.Configuration;

/// <summary>
/// Swagger config
/// </summary>
public static class SwaggerConfig
{
    /// <summary>
    /// Add swagger config
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Daisy Pets",
                    Version = "v1",
                    Description = "API da aplicação Daisy Pets.",
                    Contact = new OpenApiContact
                    {
                        Name = "Fausto Luís",
                        Email = "fauxtix.luix@gmail.com",
                        Url = new Uri("https://github.com/fsandrade")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "OSD",
                        Url = new Uri("https://opensource.org/osd")
                    },
                    TermsOfService = new Uri("https://opensource.org/osd")
                });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insira o token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Bearer"
                        }
                    },
                        Array.Empty<string>()
                    }
            });

            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath);
            //xmlPath = Path.Combine(AppContext.BaseDirectory, "DaisyPets.Api.xml");
            //c.IncludeXmlComments(xmlPath);
        });
    }

    /// <summary>
    /// Swagger configuration
    /// </summary>
    /// <param name="app"></param>
    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("./swagger/v1/swagger.json", "DP V1");
        });
    }
}