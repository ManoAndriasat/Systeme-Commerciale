using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SystemeCommerciale.Models;
using connex;
using System;
using tools;
namespace SystemeCommerciale.Controllers;

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
    public IActionResult LoginDepartement()
    {
        GetDonnees getDonnees = new GetDonnees();
        List<Departement> departements = getDonnees.getAllDepartement();
        return View("../Home/LoginDepartement",departements);
    }
    
    public IActionResult LoginChef()
    {
        GetDonnees getDonnees = new GetDonnees();
        List<Chef> chefs = getDonnees.getAllChef();
        return View("../Home/LoginChef",chefs);
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
