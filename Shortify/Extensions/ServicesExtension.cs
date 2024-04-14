using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shortify.Data;
using Shortify.Data.Abstractions;
using Shortify.Data.Mapping;
using Shortify.Data.Repository;
using Shortify.Services;

namespace Shortify.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseMySql(
                configuration.GetConnectionString("MySql"),
                new MySqlServerVersion(new Version("8.3.0"))));

            services.AddHttpContextAccessor();

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LinkMappingProfile(provider.GetService<IHttpContextAccessor>()!));
            }).CreateMapper());

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<ILinkService, LinkService>(provider =>
            {
                var unitOfWork = provider.GetService<IUnitOfWork>()!;

                var mapper = provider.GetService<IMapper>()!;

                var httpContext = provider.GetRequiredService<IHttpContextAccessor>()
                                          .HttpContext!;

                var address = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

                return new LinkService(unitOfWork, mapper, address);
            });

            return services;
        }
    }
}
