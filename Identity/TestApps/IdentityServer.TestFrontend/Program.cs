using Identity.ClientLib;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication((options) =>
{
    options.DefaultScheme = "LocalCookies";
    options.DefaultChallengeScheme = "SSOServer";

})
.AddCookie("LocalCookies", (cookieOptions) =>
{
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(10);
})
.AddOpenIdConnect("SSOServer", oidcOptions =>
{
    var settings = builder.Configuration.GetSection("OidcSettings").Get<OidcConfigSettings>();

    oidcOptions.Authority = settings.Authority;
    oidcOptions.ClientId = settings.ClientID;
    settings.Scopes.ForEach(s => oidcOptions.Scope.Add(s));
    oidcOptions.ClientSecret = settings.ClientSecret;

    oidcOptions.GetClaimsFromUserInfoEndpoint = true;
    oidcOptions.ResponseType = "code";

    oidcOptions.TokenValidationParameters.NameClaimType = "name";
    oidcOptions.TokenValidationParameters.RoleClaimType = "role";

    oidcOptions.GetClaimsFromUserInfoEndpoint = true;
    oidcOptions.SaveTokens = true;

    oidcOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        NameClaimType = "name",
        RoleClaimType = "role"
    };
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Danger");
});




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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
