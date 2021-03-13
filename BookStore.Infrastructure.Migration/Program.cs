using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookStore.Infrastructure.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using var scope = serviceProvider.CreateScope();

            UpdateDatabase(scope.ServiceProvider);
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString("Data Source=C:\\Users\\rafael\\Desktop\\projects\\BookStore\\BookStore.WebAPI\\BookStore.db")
                    .ScanIn(typeof(AddUsersTable).Assembly).For.Migrations()
                    .ScanIn(typeof(AddAuthorsTable).Assembly).For.Migrations()
                    .ScanIn(typeof(AddBooksTable).Assembly).For.Migrations()
                 )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
