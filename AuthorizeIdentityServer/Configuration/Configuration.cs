using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizeIdentityServer.Configuration
{
    public static class Configuration
    {
        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "ChatClient",
                    RequireClientSecret =false,
                    RequireConsent =false,
                    RequirePkce = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedCorsOrigins = {"http://localhost:3000"},
                    RedirectUris ={ "http://localhost:3000/callback" },
                    AllowedScopes =
                    {
                        "chatHub",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
               {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile()
               };
        }

        internal static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("chatHub")
        };

        internal static IEnumerable<ApiScope> GetApiScopes()=>
        
        new List<ApiScope>
        {
            new ApiScope("chatHub","chatHub")
        };

    }
}

