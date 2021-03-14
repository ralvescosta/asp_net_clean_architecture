using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

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
            var basePath = Environment.CurrentDirectory;
            var absolute_path = Path.GetFullPath("..\\..\\..\\..\\BookStore.WebAPI\\BookStore.db", basePath);

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString($"Data Source={absolute_path}")
                    .ScanIn(typeof(AddUsersTable).Assembly).For.Migrations()
                    .ScanIn(typeof(AddAuthorsTable).Assembly).For.Migrations()
                    .ScanIn(typeof(AddBooksTable).Assembly).For.Migrations()
                    .ScanIn(typeof(AddUsersBooksTable).Assembly).For.Migrations()
                 )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
