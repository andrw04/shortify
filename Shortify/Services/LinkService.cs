﻿using AutoMapper;
using FluentValidation;
using Shortify.Data.Abstractions;
using Shortify.Data.Entities;
using Shortify.Data.Mapping.DTOs;

namespace Shortify.Services
{
    public class LinkService : ILinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<LinkDto> _validator;

        public LinkService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LinkDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task CreateLinkAsync(LinkDto link, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(link, cancellationToken);

            var identifier = Randomizer.GetRandomString();

            var links = await _unitOfWork.LinkRepository.GetAsync(cancellationToken,
                l => l.Id == identifier);
            var existsLink = links.FirstOrDefault();

            if (existsLink is not null)
            {
                throw new ArgumentException("This URL already exists.");
            }

            link.Id = identifier;

            await _unitOfWork.LinkRepository.AddAsync(_mapper.Map<Link>(link));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task DeleteLinkAsync(string id, CancellationToken cancellationToken = default)
        {
            var linkDto = await GetLinkByIdAsync(id, cancellationToken);

            var link = _mapper.Map<Link>(linkDto);

            await _unitOfWork.LinkRepository.DeleteAsync(link, cancellationToken);
            await _unitOfWork.SaveAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<LinkDto>> GetAllLinksAsync(CancellationToken cancellationToken = default)
        {
            var links = await _unitOfWork.LinkRepository.GetAsync(cancellationToken);

            var linkDtos = links.Select(l => _mapper.Map<LinkDto>(l));

            return linkDtos;
        }

        public async Task<LinkDto> GetLinkDtoByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var existsLink = await GetLinkByIdAsync(id, cancellationToken);

            return _mapper.Map<LinkDto>(existsLink);
        }

        public async Task UpdateLinkAsync(string id, LinkDto link, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(link, cancellationToken);

            var existsLink = await GetLinkByIdAsync(id, cancellationToken);

            existsLink.LongURL = link.LongURL;

            await _unitOfWork.LinkRepository.UpdateAsync(existsLink,
                cancellationToken);

            await _unitOfWork.SaveAllAsync();
        }

        public async Task IncreaseClickCountAsync(string id, CancellationToken cancellationToken = default)
        {
            var linkDto = await GetLinkByIdAsync(id, cancellationToken);

            linkDto.ClickCount += 1;

            await _unitOfWork.LinkRepository.UpdateAsync(_mapper.Map<Link>(linkDto),
                cancellationToken);

            await _unitOfWork.SaveAllAsync();
        }

        private async Task<Link> GetLinkByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var links = await _unitOfWork.LinkRepository.GetAsync(cancellationToken,
                l => l.Id == id);
            var existsLink = links.FirstOrDefault();

            if (existsLink == null)
            {
                throw new ArgumentException("URL is not exists.");
            }

            return existsLink;
        }
    }
}
