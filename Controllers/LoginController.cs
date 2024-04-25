using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SystemeCommerciale.Models;
using connex;
using System;
using tools;
namespace SystemeCommerciale.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public IActionResult loginDeptartementTraitement(IFormCollection form)
    {   
        int dept = int.Parse(form["department"]);
        GetDonnees getDonnees = new GetDonnees();
        Boolean isExist = false;
        List<Departement> departements = getDonnees.getAllDepartement();
        for (int i = 0; i < departements.Count; i++)
        {
            if (departements[i].getIdDepartement()== dept)
            {
                isExist = true;
                HttpContext.Session.SetString("session","D"+dept);
            }
        }
        if(isExist){
            
        }else{
            return Redirect("NotAccess");
        }
        return Redirect("../DemandeBesoin");
    }
    
    public IActionResult loginChefTraitement(IFormCollection form)
    {
        int chef = int.Parse(form["chef"]);
        GetDonnees getDonnees = new GetDonnees();
        Boolean isExist = false;
        List<Chef> chefs = getDonnees.getAllChef();
        for (int i = 0; i < chefs.Count; i++)
        {
            if (chefs[i].idChef== chef)
            {
                isExist = true;
                HttpContext.Session.SetString("session","C"+chef);
            }
        }
        if(isExist){
            
        }else{
            return Redirect("NotAccess");
        }
        return View("../Home/Welcome");
    }
    public IActionResult NotAccess(){
        return View("../NotAccess/NoAccess");
    }

}
