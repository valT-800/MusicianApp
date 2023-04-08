using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;

namespace MusicianAppUI

{
  public static class RegisterServices
  {
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
      // Add services to the container.
     
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
      builder.Services.AddMemoryCache(); // Caching
      
      // Custom UI Library
      builder.Services.AddMudServices();
      builder.Services.AddMudBlazorDialog();
      
      
      // UI LOGIN generated
      builder.Services.AddControllers();
      builder.Services.AddLocalization(options =>
      {
        options.ResourcesPath = "Resources";
      });

      builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();


      // Registering auth system
      builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
       .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
            
      // Role workaround
      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("Admin", policy =>
        {
          policy.RequireClaim("jobTitle", "Admin");
        });
      });

      

      // Scoped once per user 
      // Transient 
      // Sharing data
      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<ITrackData, MongoTrackData>(); 
      builder.Services.AddSingleton<IPlaylistData, MongoPlaylistData>();       
      builder.Services.AddSingleton<IRoomData, MongoRoomData>();       
      builder.Services.AddSingleton<IStatusData, MongoStatusData>();
      builder.Services.AddSingleton<IInternalAuditData, MongoInternalAuditData>();
      builder.Services.AddSingleton<IUserData, MongoUserData>();
    }
    public static RequestLocalizationOptions GetLocalizationOptions(this WebApplicationBuilder builder)
    {
      var cultures = builder.Configuration.GetSection("Cultures")
        .GetChildren().ToDictionary(x => x.Key, x => x.Value);
      
      var supportedCultures = cultures.Keys.ToArray();
    
      var localizationOptions = new RequestLocalizationOptions()
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
      return localizationOptions;
    }
  }
}
