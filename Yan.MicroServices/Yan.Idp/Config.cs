// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Yan.Idp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("article","My Artilce"),
                new ApiScope("system","My System"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api1","#api1")
                {
                    Scopes = { "api1.weather.scope", "api1.test.scope" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "postman client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("postman secret".Sha256()) },

                    AllowedScopes = { "api1.weather.scope", "api1.test.scope" },
                },
                new Client
                {
                    ClientId="Yan.MvcClient",
                    ClientSecrets={new Secret("Yan.MvcClient".Sha256())},

                    AllowedGrantTypes=GrantTypes.Code,

                    RedirectUris={ "http://localhost:9898/signin-oidc" },
                    PostLogoutRedirectUris={ "http://localhost:9898/signout-callback-oidc" },

                    AllowOfflineAccess=true,

                    AllowedScopes=new  List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "article",
                        "system"
                    }

                }
            };
    }
}