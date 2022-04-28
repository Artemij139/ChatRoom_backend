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
                    ClientId = "clientId",
                    ClientSecrets = { new Secret("clientSecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "ChatSpa"
                    }
                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
           {

              new IdentityResources.OpenId()

           };
        }

        internal static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("ChatSpa")
        };

    }
}

