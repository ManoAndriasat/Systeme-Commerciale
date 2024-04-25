using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemeCommerciale;
using connex;
using System;
using tools;

public class Validation : Controller
{
    public IActionResult Index() {
        if (HttpContext.Session.GetString("session").Equals("C3", StringComparison.OrdinalIgnoreCase))
        {
            GetDonnees getDonnees = new GetDonnees();
            List<DemandeBesoin> demandeBesoins = getDonnees.getAllDemandeNonVal();
            return View(demandeBesoins);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    public IActionResult ValideDemandeBesoin(int besoin ){
        if (HttpContext.Session.GetString("session").Equals("C3", StringComparison.OrdinalIgnoreCase))
        {
            GetDonnees getDonnees = new GetDonnees();
            DemandeBesoin demandebesoin = getDonnees.getDemandeNonValById(besoin);
            InsertDonnees insertDonne = new InsertDonnees();
            if(demandebesoin!=null){
                insertDonne.updateEtatDemandeBesoinToValide(demandebesoin);
                insertDonne.insertToRegroupementBesoin(demandebesoin);
            }
            return Redirect(".");
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    public IActionResult RefuseDemandeBesoin(int besoin ){
        if (HttpContext.Session.GetString("session").Equals("C3", StringComparison.OrdinalIgnoreCase))
        {
            GetDonnees getDonnees = new GetDonnees();
            DemandeBesoin demandebesoin = getDonnees.getDemandeNonValById(besoin);
            InsertDonnees insertDonne = new InsertDonnees();
            if(demandebesoin!=null){
                insertDonne.updateEtatDemandeBesoinToNonValide(demandebesoin);
            }
            return Redirect(".");
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
}
