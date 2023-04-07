using System;
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
    }
}