using FluentValidation;
using Shortify.Data.Mapping.DTOs;

namespace Shortify.Data.Validators
{
    public class LinkValidator : AbstractValidator<LinkDto>
    {
        public LinkValidator()
        {
            RuleFor(link => link.LongURL)
                .NotEmpty()
                .WithMessage("URL не должен быть пустым.")
                .MustAsync(UrlExistsAsync)
                .WithMessage("URL не существует или недоступен.");
        }

        private async Task<bool> UrlExistsAsync(string url, CancellationToken cancellationToken)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
