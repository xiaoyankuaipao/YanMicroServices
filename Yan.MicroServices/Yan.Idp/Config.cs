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
                    ClientId="vue-manage", //后台管理系统vue spa client
                    ClientName="Yan Spa client",
                    ClientSecrets = new [] { new Secret("spasecret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes=new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,//如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        "system","article"
                    },
                    AlwaysIncludeUserClaimsInIdToken=true,//
                    AccessTokenLifetime=3600,
                    AllowAccessTokensViaBrowser=true,//
                    RefreshTokenUsage=TokenUsage.ReUse,//
                    AllowOfflineAccess=true,//如果要获取refresh_token ,必须把AllowOfflineAccess设置为true
                    RefreshTokenExpiration=TokenExpiration.Absolute,//刷新令牌将在固定的时间点上过期 //
                    AbsoluteRefreshTokenLifetime=3600*24*7,//刷新令牌将在固定的时间点上过期 //
                },
                new Client
                {
                    ClientId="vue-blog",
                    ClientName="Spa Client",
                    //ClientUri="http://localhost:8080",
                    ClientUri="http://118.24.205.200",
                    AllowedGrantTypes=GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser=true,
                    RequireConsent=false,
                    AccessTokenLifetime=60*5,
                    AlwaysIncludeUserClaimsInIdToken=true,
                    //RedirectUris={
                    //    "http://localhost:8080/signin-oidc",//登录成功之后，跳转回来的uri
                    //    "http://localhost:8080/redirect-silentrenew"//用于刷新token的uri
                    //},

                    //PostLogoutRedirectUris={
                    //    "http://localhost:8080"//登出之后，跳转的uri
                    //},

                    RedirectUris={
                        "http://118.24.205.200/signin-oidc",//登录成功之后，跳转回来的uri
                        "http://118.24.205.200/redirect-silentrenew"//用于刷新token的uri
                    },

                    PostLogoutRedirectUris={
                        "http://118.24.205.200"//登出之后，跳转的uri
                    },

                    AllowedCorsOrigins={
                        "http://118.24.205.200",
                        "http://localhost:8080"
                    },

                    AllowedScopes={
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        "system","article"
                    }
                },
                new Client
                {
                    ClientId="Yan.MvcClient",
                    ClientName="Yan.MvcClient",
                    ClientSecrets={new Secret("Yan.MvcClient".Sha256())},

                    AllowedGrantTypes=GrantTypes.Code,
                    RequireConsent=false,
                    AlwaysIncludeUserClaimsInIdToken=true,

                    RedirectUris={ "http://118.24.205.200:9898/signin-oidc" },
                    FrontChannelLogoutUri="http://118.24.205.200:9898/signout-oidc",
                    PostLogoutRedirectUris={ "http://118.24.205.200:9898/signout-callback-oidc" },

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