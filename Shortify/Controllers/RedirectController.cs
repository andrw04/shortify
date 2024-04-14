using Microsoft.AspNetCore.Mvc;
using Shortify.Services;

namespace Shortify.Controllers
{
    public class RedirectController : Controller
    {
        private readonly ILinkService _linkService;

        public RedirectController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RedirectUrl(string id)
        {
            var link = await _linkService.GetLinkDtoByIdAsync(id);

            if (link is not null)
            {
                await _linkService.IncreaseClickCountAsync(id);

                return Redirect(link.LongURL);
            }

            return NotFound();
        }
    }
}
