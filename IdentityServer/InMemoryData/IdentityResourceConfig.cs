using System.Collections.Generic;
using Duende.IdentityServer.Models;

namespace IdentityServer.InMemoryData
{
    public static class IdentityResourceConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource []
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("battleship.user", "Basic Battleship Player"),
                new ApiScope("battleship.api", "API Scope for the API"),
  new ApiScope("battleship.Admin", "Battleship user with elevated privilages"),
            };
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("BattleshipAPI")
            {
                Scopes = new List<string> { "battleship.api", "battleship.admin"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "battleship.api.client",
                    ClientName = "Battleship API-API Credentials",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                    AllowedScopes = { "battleship.api", "battleship.Admin" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "Battleship.user",
                    ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {"https://localhost:5444/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "battleship.user", "battleship.admin"},
                    RequirePkce = true,
                    RequireConsent = false,
                    AllowPlainTextPkce = false
                },
            };
    }
}