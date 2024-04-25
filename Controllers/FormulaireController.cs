using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace connex;


public class FormulaireController : Controller
{
    public IActionResult insertDemandeBesoin(IFormCollection form)
    {
        double quantite = double.Parse(form["quantite"]);
        int produit = int.Parse(form["produit"]);
        int departement = int.Parse(form["department"]);
        InsertDonnees insertDonne = new InsertDonnees();
        insertDonne.insertDemandeBesoin(departement,produit,quantite);
        return Redirect("../DemandeBesoin/Index");
    }
}
