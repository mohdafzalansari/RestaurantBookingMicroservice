using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Identity.Infrastructure.Configuration
{
    public static class Config
    {
        //define the list of Api resource(list of microservices)
        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("RestaurentApi.read"),
                new ApiScope("RestaurentApi.write")
            };
        }
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("RestaurentApi")
                {
                    Scopes = new List<string>{ "RestaurentApi.read", "RestaurentApi.write" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
            };
        }


        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients(/*Dictionary<string, string> clientsUrl*/)
        {
            return new List<Client>
            {
                new Client
                {
                    Enabled=true,
                    ClientId = "RestApiClient",
                    ClientName = "Rest Api Client Local Dev",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent=false,
                    ClientSecrets=new List<Secret> {new Secret("secret".Sha256())},
                    AllowedScopes = { "RestaurentApi.read" }
                }
            };
        }
    }
}
