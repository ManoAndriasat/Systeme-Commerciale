using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemeCommerciale;
using connex;
using System;
using tools;

public class Regroupement : Controller
{
    public IActionResult Index() {
            GetDonnees getDonnees = new GetDonnees();
            List<Produit> produits = getDonnees.getAllProduit();
            List<RegroupementBesoinModel> allRegroupementbesoin = new List<RegroupementBesoinModel>();
            for(int i = 0 ; i < produits.Count ; i++){
                List<VRegroupementBesoin> regroupement_besoin = getDonnees.getAllRegroupememtBesoin(produits[i].getIdProduit());
                if(regroupement_besoin.Count>0){
                    RegroupementBesoinModel regroupementbesoin = new RegroupementBesoinModel();
                    regroupementbesoin.setProduit(produits[i]);
                    regroupementbesoin.setRegroupementBesoin(regroupement_besoin);
                    regroupementbesoin.setquantiteTotal(getDonnees.getQuantiteTotalDemandeBesoinByProduct(produits[i].getIdProduit()));
                    allRegroupementbesoin.Add(regroupementbesoin);
                }
            }
        if (HttpContext.Session.GetString("session").Equals("C3", StringComparison.OrdinalIgnoreCase))
        {
            return View(allRegroupementbesoin);
        }else if(HttpContext.Session.GetString("session").Equals("C1", StringComparison.OrdinalIgnoreCase)){
            return View("IndexFinance",allRegroupementbesoin);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
}
