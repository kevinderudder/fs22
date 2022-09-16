using System;
using System.Linq;
using System.Reflection;
using System.IO;

using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder ConfigBuilder = new ConfigurationBuilder();
            ConfigBuilder.SetBasePath(Assembly.GetExecutingAssembly().Location.Replace("\\EFCoreMigrations.dll", ""))
                         .AddJsonFile("appsettings.json");
            IConfiguration Configuration = ConfigBuilder.Build();

            var serviceProvider = CreateServices(Configuration);

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(IConfiguration Configuration)
        {
            var DBConnString = Configuration.GetSection("ConnectionStrings")["DBConnString"];

            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLServer support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(DBConnString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            bool dropDB = false;
            if (!dropDB)
                runner.MigrateUp();
            else
                runner.MigrateDown(0);
        }
    }
}
