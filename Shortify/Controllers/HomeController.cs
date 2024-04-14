using Microsoft.AspNetCore.Mvc;
using Shortify.Data.Mapping.DTOs;
using Shortify.Models;
using Shortify.Services;
using System.Diagnostics;
using System.Text.Json;

namespace Shortify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILinkService _linkService;

        public HomeController(ILogger<HomeController> logger, ILinkService linkService)
        {
            _logger = logger;
            _linkService = linkService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _linkService.GetAllLinksAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateEdit");
        }

        [HttpPost]
        public async Task<IActionResult> Create(LinkDto link)
        {
            await _linkService.CreateLinkAsync(link);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var link = await _linkService.GetLinkDtoByIdAsync(id);

            return View("CreateEdit", link);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LinkDto link)
        {
            await _linkService.UpdateLinkAsync(link.Id, link);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _linkService.DeleteLinkAsync(id);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(string messageJson = "")
        {
            var erorrMessage = JsonSerializer.Deserialize<ErrorViewModel>(messageJson);
            ViewData["errorMessage"] = erorrMessage;

            return View("Index");
        }
    }
}
