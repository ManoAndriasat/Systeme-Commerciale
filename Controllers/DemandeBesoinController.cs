using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemeCommerciale;
using connex;
using System;
using tools;

public class DemandeBesoinController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("session").StartsWith("D", StringComparison.OrdinalIgnoreCase))
        {
            GetDonnees getDonnees = new GetDonnees();
            DemandeBesoinFormModel data_demande_besoin = new DemandeBesoinFormModel();
            List<Departement> departements = getDonnees.getAllDepartement();
            List<Produit> produits = getDonnees.getAllProduit();
            data_demande_besoin.departements = departements;
            data_demande_besoin.produits = produits;
            return View(data_demande_besoin);
        }else{
            return Redirect("../Login/NotAccess");
        }
    }
    public IActionResult Proforma()
    {
        return View("../proforma/Index");
    }
}
