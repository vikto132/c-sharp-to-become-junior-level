using System.Collections.Generic;
using Core.Configuration;
using Core.Data;
using Core.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Exceptions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, DbConfiguration options)
    {
        services.TryAddScoped<DbContextContainer>();
        if (!options.Junior.IsNullOrEmpty())
        {
            services.AddDbContext<JuniorDbContext>();
        }
        services.AddScoped<IGeneralModelRepository, GeneralModelRepository>();
        return services;
    }
}