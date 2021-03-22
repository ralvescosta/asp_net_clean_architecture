using BookStore.Application.Interfaces;
using BookStore.Application.UseCase;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Database;
using BookStore.Infrastructure.Interfaces;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Services;
using BookStore.Shared.Configurations;
using BookStore.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.IoC
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationUseCase, AuthenticationUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<ISessionUseCase, SessionUseCase>();
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddScoped<IAuthorUseCase, AuthorUseCase>();
            services.AddScoped<IBookUseCase, BookUseCase>();
            services.AddScoped<IBorrowBookUseCase, BorrowBookUseCase>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IUserBookRepository, UserBookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHasherService, HasherService>();
            services.AddScoped<ITokenManagerService, TokenManagerService>();
            services.AddSingleton<IDbContext, SQLiteDbContext>();
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services) 
        {
            services.AddSingleton<IConfigurations, Configurations>();
            return services;
        }
    }
}
