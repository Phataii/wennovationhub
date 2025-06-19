using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using wennovation_hub.Models;

namespace wennovation_hub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("services")]
    public IActionResult Services()
    {
        return View();
    }

    [Route("impact")]
    public IActionResult Impact()
    {
        return View();
    }

    [Route("gallery")]
    public IActionResult Gallery()
    {
        return View();
    }

    [Route("bookings")]
    public IActionResult Booking()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
