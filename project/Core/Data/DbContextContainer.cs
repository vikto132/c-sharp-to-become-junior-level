using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Data;

public class DbContextContainer
{
    private static ConcurrentDictionary<Type, Type> _entityMappingDictionary = new ConcurrentDictionary<Type, Type>();

    private readonly IServiceProvider _serviceProvider;
    private readonly JuniorDbContext _juniorDbContext;
    
    public DbContextContainer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public JuniorDbContext Junior
    {
        get
        {
            if (_juniorDbContext == null)
            {
                return _serviceProvider.GetService<JuniorDbContext>();
            }

            return _juniorDbContext;
        }
    }
    
    public DbContext GetContextFor<TEntity>()
    {
        return GetContextFor(typeof(TEntity));
    }
    
    public DbContext GetContextFor(Type entityType)
    {
        var contextType = _entityMappingDictionary.GetOrAdd(entityType, MappingDictionary_GetOrAdd);
        if (contextType == null)
        {
            throw new InvalidOperationException($"No context is available for entity type {entityType.GetClassDescription()}");
        }
        return (DbContext)_serviceProvider.GetService(contextType);
    }

    private static Type MappingDictionary_GetOrAdd(Type entityType)
    {
        var contextTypes = new[]
        {
            typeof(JuniorDbContext)
        };
        foreach(var contextType in contextTypes)
        {
            var result = IsEntityMapped(entityType, contextType);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    
    private static Type IsEntityMapped(Type entityType, Type contextType)
    {
        var properties = contextType.GetProperties();
        var isMapped = properties.Any(prop => prop.PropertyType.IsGenericType
                                              && prop.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                                              && prop.PropertyType.GetGenericArguments().Length == 1
                                              && prop.PropertyType.GetGenericArguments().First() == entityType);
        return isMapped ? contextType : null;
    }
}