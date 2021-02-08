using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookStore.Infrastructure.IoC
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            //services.AddScoped<IRepository, Repository>()
            return services;
        }
    }
}
