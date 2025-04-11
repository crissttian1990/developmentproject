using System.Diagnostics;
using DevelopmentProject.Models;
using DevelopmentProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScraperService _scraperService;

        public HomeController(ILogger<HomeController> logger, IScraperService scraperService)
        {
            _logger = logger;
            _scraperService = scraperService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scraper()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScraperResponse(ScraperRequestModel request)
        {
            var result = await _scraperService.NumberOfResultsOnGoogleAsync(request);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
