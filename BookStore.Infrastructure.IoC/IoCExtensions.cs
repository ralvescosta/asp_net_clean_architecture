using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.IoC
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHasher, Hasher>();
            return services;
        }
    }
}
