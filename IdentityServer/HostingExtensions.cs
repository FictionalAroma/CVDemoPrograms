using System;
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
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<IdentityServerContext>(dbBuilder =>
            {
                dbBuilder.UseSqlite(builder.Configuration.GetConnectionString("IdentityServerContextConnection"));
            });

            builder.Services.AddIdentity<LDSSOUser, IdentityRole>()
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
                .AddAspNetIdentity<LDSSOUser>()
                .AddInMemoryIdentityResources(InMemoryData.IdentityResourceConfig.IdentityResources)
                .AddInMemoryApiScopes(InMemoryData.IdentityResourceConfig.ApiScopes)
                .AddInMemoryClients(InMemoryData.IdentityResourceConfig.Clients)
                .AddInMemoryApiResources(InMemoryData.IdentityResourceConfig.ApiResources);

#if DEBUG
            builder.Services.AddScoped<InMemoryData.DataSeeder>();
#endif

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
            //app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages().RequireAuthorization();
            });
            return app;
        }

        public static WebApplication DataSeeding(this WebApplication app)
        {
#if DEBUG
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetService<InMemoryData.DataSeeder>();

            seeder?.RunDefaultUserSeeding();
#endif
            return app;
        }
    }

}