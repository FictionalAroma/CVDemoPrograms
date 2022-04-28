using Serilog;

namespace IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // uncomment if you want to add a UI
            //builder.Services.AddRazorPages();

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

                
                .AddInMemoryIdentityResources(InMemoryData.IdentityResourceConfig.IdentityResources)
                .AddInMemoryApiScopes(InMemoryData.IdentityResourceConfig.ApiScopes)
                .AddInMemoryClients(InMemoryData.IdentityResourceConfig.Clients)
                .AddInMemoryApiResources(InMemoryData.IdentityResourceConfig.ApiResources)
                .AddTestUsers(InMemoryData.TestUserStore.Users);


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
            //app.UseStaticFiles();
            //app.UseRouting();

            app.UseIdentityServer();

            // uncomment if you want to add a UI
            //app.UseAuthorization();
            //app.MapRazorPages().RequireAuthorization();

            return app;
        }
    }
}