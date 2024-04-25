using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tools;
using connex;
namespace SystemeCommerciale;

public class Proforma : Controller
{

    public IActionResult ListProforma(int id_produit)
    {
        if (HttpContext.Session.GetString("session").Equals("C1", StringComparison.OrdinalIgnoreCase))
        {
            GetDonnees getDonnees = new GetDonnees();
            ProformaModel proforma_model = new ProformaModel();
            proforma_model.setProduit(getDonnees.getProduitById(id_produit));
            proforma_model.setVStockProduitFournisseur(getDonnees.getProformaByProduit(id_produit));
            return View("../proforma/Index",proforma_model);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
}
