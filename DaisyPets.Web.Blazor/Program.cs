using DaisyPets.Web.Blazor;
using DaisyPets.Web.Blazor.Shared;
using Serilog;
using Syncfusion.Blazor;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json")
                      .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
   .CreateLogger();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSyncfusionBlazor(options => { options.Animation = GlobalAnimationMode.Enable; });

builder.Services.AddCors();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

// TODO Test (not used for now)
//builder.Services.AddScoped<IApiClientWrapperService<VacinaDto>>(c => new ApiClientWrapperService<VacinaDto>("", ""));

builder.Services.RegisterServices();

Syncfusion.Licensing.SyncfusionLicenseProvider
    .RegisterLicense("Ngo9BigBOggjHTQxAR8 / V1NGaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUH5acXBRQGVZWUdxVw ==");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



#region Localization


// defines the list of cultures that the app will support
var supportedCultures = new[] { "en-GB", "es", "fr", "pt-PT" };

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization("pt-PT");

#region Localization

app.UseRequestLocalization(localizationOptions);

#endregion


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Nzc5NTM3QDMyMzAyZTMzMmUzMGI2eGIzS0JKL2lpZGllaytQQ2NvcHFYN2dXWG5tcUtyRDdoODI1TEpsOEU9");

#endregion

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();

});

//app.UseAuthentication();
//app.UseAuthorization();

// app.UseSerilogRequestLogging();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
