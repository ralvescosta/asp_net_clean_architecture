using BookStore.Infrastructure.IoC;
using BookStore.WebAPI.Extensions;
using BookStore.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BookStore.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRepositories();
            services.AddApplications();
            services.AddConfigurations();
            services.AddSwagger();
            services.AddCustomInvalidModelStateResponse();
            services.AddAuthentication("Authentication")
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("Authentication", null);
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreBook API");
            });

            //var builder = new RouteBuilder(app);
            //builder.MapMiddlewarePost("/api/v1/book", appBuilder => {
            //    appBuilder.Use(Middleware);
            //});
        }
    }
}
