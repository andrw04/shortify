using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shortify.Data;
using Shortify.Data.Abstractions;
using Shortify.Data.Mapping;
using Shortify.Data.Mapping.DTOs;
using Shortify.Data.Repository;
using Shortify.Data.Validators;
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

            services.AddScoped<IValidator<LinkDto>, LinkValidator>();

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<ILinkService, LinkService>(provider =>
            {
                var unitOfWork = provider.GetService<IUnitOfWork>()!;

                var mapper = provider.GetService<IMapper>()!;

                var validator = provider.GetService<IValidator<LinkDto>>()!;

                return new LinkService(unitOfWork, mapper, validator);
            });

            return services;
        }
    }
}
