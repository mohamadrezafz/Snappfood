

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Snappfood.Domain.Entities;
using Snappfood.Application.Models.Request;
using Snappfood.Application.Models.Validation;


namespace Snappfood.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ProductCreate>, ProductCreateValidator>();

        return services;
    }
}