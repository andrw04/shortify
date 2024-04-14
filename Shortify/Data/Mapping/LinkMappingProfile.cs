using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shortify.Data.Entities;
using Shortify.Data.Mapping.DTOs;

namespace Shortify.Data.Mapping
{
    public class LinkMappingProfile : Profile
    {
        public LinkMappingProfile(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext!;
            var baseAddress = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            CreateMap<Link, LinkDto>()
                .ForMember(dist => dist.ShortURL, opt => opt.MapFrom(src => $"{baseAddress}/{src.Id}"));
            CreateMap<LinkDto, Link>();
        }
    }
}
