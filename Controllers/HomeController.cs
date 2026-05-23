using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FootballScoreApp.Models;

namespace FootballScoreApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}
