var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication((options) =>
{
    options.DefaultScheme = "LocalCookies";
    options.DefaultChallengeScheme = "SSOServer";

})
.AddCookie("LocalCookies", (cookieOptions) =>
{
    cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(10);
})
.AddOpenIdConnect(oidcOptions =>
{
    oidcOptions.Authority = "identiyurlhere";

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
