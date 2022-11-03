using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Web.Filters;
using FamilyPlaner.Web.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace FamilyPlaner.Web;

static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        services.AddControllers(o => o.Filters.Add<ApiExceptionFilterAttribute>());

        services
            .AddFluentValidationClientsideAdapters();

        services.AddRazorPages();

        services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Family planer API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }
}