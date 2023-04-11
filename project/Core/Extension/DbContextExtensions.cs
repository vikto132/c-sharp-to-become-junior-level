using System;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Core.Extension
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContext<TDbContext>(this IServiceCollection services,
            string connectionString)
            where TDbContext : DbContext
        {
            var options = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(connectionString, builder => builder.MigrationsAssembly("Migrations"))
#if DEBUG
                .LogTo(Console.WriteLine, LogLevel.Information)
#endif
                .Options;
            services.AddSingleton(options);
            services.AddScoped<TDbContext>();
            return services;
        }
        
        public static async Task<TEntity> GetByIdOr404Async<TEntity>(this DbContext context, long id,
            bool raiseException = true, params string[] includeNavigationPaths)
            where TEntity : class, IHasId
        {
            var queryable = context.Set<TEntity>().AsQueryable();
            foreach (var includeNavigationPath in includeNavigationPaths)
            {
                queryable = queryable.Include(includeNavigationPath);
            }

            var entity = await queryable.SingleOrDefaultAsync(x => x.Id == id);
            if (raiseException && entity == null)
            {
                throw new NotFoundException(
                    $"The {typeof(TEntity).GetClassDescription()} with id '{id}' cannot be found.");
            }

            return entity;
        }
    }
}