using System.Reflection;
using Identity.DataAccess.DataContext;
using Identity.DataAccess.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // uncomment if you want to add a UI
            //builder.Services.AddRazorPages();

            builder.Services.AddDbContext<IdentityServerContext>(dbBuilder =>
            {
                dbBuilder.UseSqlite(builder.Configuration.GetConnectionString("IdentityServerContextConnection"));
            });

            builder.Services.AddIdentity<IdentityServerUser, IdentityRole>(options =>
                {
                    
                })
                .AddEntityFrameworkStores<IdentityServerContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddIdentityServer(options =>
                {
                    // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                    options.EmitStaticAudienceClaim = true;

                    // event raising
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseSuccessEvents = true;



                })
                .AddDefaultEndpoints()
                .AddAspNetIdentity<IdentityServerUser>()
                .AddInMemoryIdentityResources(InMemoryData.IdentityResourceConfig.IdentityResources)
                .AddInMemoryApiScopes(InMemoryData.IdentityResourceConfig.ApiScopes)
                .AddInMemoryClients(InMemoryData.IdentityResourceConfig.Clients)
                .AddInMemoryApiResources(InMemoryData.IdentityResourceConfig.ApiResources);


            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add a UI
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment if you want to add a UI
            app.UseAuthorization();
            //app.MapRazorPages().RequireAuthorization();

            return app;
        }
    }
}