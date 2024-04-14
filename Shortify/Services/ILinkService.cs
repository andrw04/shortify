using Shortify.Data.Mapping.DTOs;

namespace Shortify.Services
{
    public interface ILinkService
    {
        Task<IEnumerable<LinkDto>> GetAllLinksAsync(CancellationToken cancellationToken = default);
        Task<LinkDto> GetLinkDtoByIdAsync(string id, CancellationToken cancellationToken = default);
        Task CreateLinkAsync(LinkDto link, CancellationToken cancellationToken = default);
        Task DeleteLinkAsync(string id, CancellationToken cancellationToken = default);
        Task UpdateLinkAsync(string id, LinkDto link, CancellationToken cancellationToken = default);
        Task IncreaseClickCountAsync(string id, CancellationToken cancellationToken = default);
    }
}
