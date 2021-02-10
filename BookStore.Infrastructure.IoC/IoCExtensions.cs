using BookStore.Application.Services;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookStore.Infrastructure.IoC
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUser, RegisterUser>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            //services.AddScoped<IRepository, Repository>()
            return services;
        }
    }
}
