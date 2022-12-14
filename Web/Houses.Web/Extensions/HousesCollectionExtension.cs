using System.Security.Claims;
using Houses.Core.Services;
using Houses.Core.Services.Contracts;
using Houses.Infrastructure.Data;
using Houses.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Houses.Web.Extensions
{
    public static class HousesServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddScoped<IPropertiesTypesService, PropertyTypeService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddSingleton<IUserIdProvider, CustomEmailProvider>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services,
            IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public class CustomEmailProvider : IUserIdProvider
        {
            public virtual string GetUserId(HubConnectionContext connection)
            {
                return connection.User.FindFirst(ClaimTypes.Email)?.Value!;
            }
        }
    }
}