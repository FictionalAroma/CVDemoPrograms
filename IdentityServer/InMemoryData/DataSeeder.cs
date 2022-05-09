using System.Linq;
using System.Security.Claims;
using Identity.DataAccess.DataContext;
using Identity.DataAccess.Objects;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.InMemoryData
{
    public class DataSeeder
    {
        private readonly IdentityServerContext _context;
        private readonly UserManager<LDSSOUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(IdentityServerContext context,
                            UserManager<LDSSOUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async void RunDefaultUserSeeding()
        {
            if (await _roleManager.FindByNameAsync(InMemoryData.IdentityResourceConfig.AdminRole) == null)
            {
                var adminRole = new IdentityRole(InMemoryData.IdentityResourceConfig.AdminRole);
                await _roleManager.CreateAsync(adminRole);
                await _roleManager.AddClaimAsync(adminRole,
                    new Claim(JwtClaimTypes.Role, InMemoryData.IdentityResourceConfig.AdminRole));

                //add role claims here

                var userRole = new IdentityRole(IdentityResourceConfig.UserRole);
                await _roleManager.CreateAsync(userRole);
                await _roleManager.AddClaimAsync(userRole,
                    new Claim(JwtClaimTypes.Role, InMemoryData.IdentityResourceConfig.UserRole));

            }


            if (!_context.Users.Any())
            {
                const string password = "!Fallout4";

                var adminTestUser = new LDSSOUser()
                {
                    UserName = "LD.Test.Admin",
                    Email = "admintest1@ld.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(adminTestUser, password);
                await _userManager.AddToRoleAsync(adminTestUser, IdentityResourceConfig.AdminRole);


                LDSSOUser player1TestUser = new()
                {
                    UserName = "LD.Test.Player1",
                    Email = "playertest1@ld.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(player1TestUser, password);
                await _userManager.AddToRoleAsync(player1TestUser, IdentityResourceConfig.UserRole);

                LDSSOUser player2TestUser = new ()
                {
                    UserName = "LD.Test.Player2",
                    Email = "playertest2@ld.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(player2TestUser, password);
                await _userManager.AddToRoleAsync(player1TestUser, IdentityResourceConfig.UserRole);

            }

        }
    }
}
