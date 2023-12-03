﻿using System;
using System.Collections.Generic;
using System.Linq;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Fwks.AspNetCore.Options;

namespace Fwks.AspNetCore.Extensions;

public static class SwaggerExtensions
{
    public static OpenApiOperation AddCorrelationIdParameter(this OpenApiOperation operation)
    {
        operation.Parameters.Add(new()
        {
            Name = "X-Correlation-Id",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema { Type = "string" },
            Required = true,
            Example = new OpenApiString(Guid.NewGuid().ToString())
        });

        return operation;
    }

    public static SwaggerGenOptions AddBearerSecurity(this SwaggerGenOptions options, Action<BearerSecurityOptions> securityOptionsAction = default)
    {
        var securityOptions = new BearerSecurityOptions();

        securityOptionsAction?.Invoke(securityOptions);

        options.AddSecurityDefinition(securityOptions.Name, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = securityOptions.Description,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "OAuth2",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = securityOptions.Name
                    }
                },
                new List<string>()
            }
        });

        return options;
    }

    public static SwaggerGenOptions AddOAuth2Security(this SwaggerGenOptions options, Action<OAuth2SecurityOptions> securityOptionsAction = default)
    {
        var securityOptions = new OAuth2SecurityOptions();

        securityOptionsAction?.Invoke(securityOptions);

        options.AddSecurityDefinition(securityOptions.Name, new OpenApiSecurityScheme
        {
            Description = securityOptions.Description,
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = OAUthFlow(),
                ClientCredentials = securityOptions.AddClientCredentials ? OAUthFlow() : default
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "OAuth2",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = securityOptions.Name
                    }
                },
                securityOptions.Scopes.Select(x => x.Key).ToList()
            }
        });

        return options;

        OpenApiOAuthFlow OAUthFlow()
        {
            return new OpenApiOAuthFlow
            {
                AuthorizationUrl = Uri("auth"),
                TokenUrl = Uri("token"),
                Scopes = securityOptions.Scopes
            };
        }

        Uri Uri(string type)
        {
            return new Uri($"{securityOptions.Authority}/protocol/openid-connect/{type}");
        }
    }

    public static IApplicationBuilder UseDefaultSwaggerUI(this IApplicationBuilder app, IApiVersionDescriptionProvider apiProvider, string title)
    {
        _ = app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.DocumentTitle = title;

                foreach (var groupName in apiProvider.ApiVersionDescriptions.Select(x => x.GroupName))
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());

                options.OAuthUsePkce();
            });

        return app;
    }
}