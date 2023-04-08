using Microsoft.AspNetCore.Rewrite;
using MusicianAppUI;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
var app = builder.Build();

//Register Syncfusion license
//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTk4MDAwQDMxMzkyZTM0MmUzMGlVT3FiUGRtWitqU0dzejQ0WDh6YkhBMTdjV1YrVzlYNG95RE5BQWlyWTQ9");



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRequestLocalization(builder.GetLocalizationOptions());


app.UseRouting();

// Order important
// Login
app.UseAuthentication();

// Check permissions
app.UseAuthorization();

// redirect to home page
app.UseRewriter(new RewriteOptions().Add(
  context =>
  {
      if (context.HttpContext.Request.Path == "/MicrosoftIdentity/Account/SignedOut")
      {
          context.HttpContext.Response.Redirect("/");
      }
  }));
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapFallbackToPage("/Track/{id?}", "/_Host");
app.Run();
