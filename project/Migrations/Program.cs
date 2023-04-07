﻿using System;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Data;
using Core.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migration.Utilities;

namespace Migrations
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                var serviceProvider = SetupServiceProvider();
                var db = serviceProvider.GetService<JuniorDbContext>();
                await Migrate(db);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                }

                Console.WriteLine(e.StackTrace);
                Environment.Exit(1);
            }
        }
        
        private static Task Migrate(DbContext dbContext)
        {
            if (dbContext == null) return Task.CompletedTask;
            return dbContext.Database.MigrateAsync();
        }
        
        private static IServiceProvider SetupServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
            AppSettingConfiguration.Inject(configuration);
            var dbConnectionStrings = configuration.GetSection<DbConfiguration>("DbConfigurations");
            return serviceCollection.AddDbContext<JuniorDbContext>(dbConnectionStrings.Connection).BuildServiceProvider();
        }
    }
    
    public abstract class AbstractDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected DbContextOptions<TContext> GetDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseSqlServer("random string", x => x.MigrationsAssembly("Migrations"));
            return builder.Options;
        }

        public abstract TContext CreateDbContext(string[] args);
    }
    
    public class JuniorDbContextFactory : AbstractDbContextFactory<JuniorDbContext>
    {
        public override JuniorDbContext CreateDbContext(string[] args) => new(GetDbContextOptions());
    }
}