using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tools;
using connex;
namespace SystemeCommerciale;

public class BandDeCommande : Controller
{

    public IActionResult GenererBandDeCommande(int id_produit,int id_fournisseur)
    {

        GetDonnees getDonnees = new GetDonnees();
        // alaina ny produit ny fournisseur where id_produit = id_produit , id_fournisseur = id_fournisseur
        VStockProduitFournisseur produit_fournisseur = null;
        for(int i = 0; i < (getDonnees.getProformaByProduit(id_produit)).Count ; i++){
            if(getDonnees.getProformaByProduit(id_produit)[i].getFournisseur().getIdFournisseur() == id_fournisseur)
            produit_fournisseur = getDonnees.getProformaByProduit(id_produit)[i];
        }
        
        // alaina koa ny regroupement ny produit
        RegroupementBesoinModel regroupementbesoin = new RegroupementBesoinModel();
        List<VRegroupementBesoin> regroupement_besoin = getDonnees.getAllRegroupememtBesoin(id_produit);
        regroupementbesoin.setProduit(getDonnees.getProduitById(id_produit));
        regroupementbesoin.setRegroupementBesoin(regroupement_besoin);
        regroupementbesoin.setquantiteTotal(getDonnees.getQuantiteTotalDemandeBesoinByProduct(id_produit));

        BandDeCommandeModel bandDeCommande = new BandDeCommandeModel();
        if(produit_fournisseur.getQuantite() > regroupementbesoin.getquantiteTotal()){
            bandDeCommande.setQuantite(regroupementbesoin.getquantiteTotal());
        }else{
            bandDeCommande.setQuantite(produit_fournisseur.getQuantite());
        }
        bandDeCommande.setProduitFournisseur(produit_fournisseur);
        bandDeCommande.setRegroupementBesoin(regroupementbesoin);
        bandDeCommande.setMontant(produit_fournisseur.getPrixUnitaire()*bandDeCommande.getQuantite());
        bandDeCommande.setReference("F"+produit_fournisseur.getFournisseur().getIdFournisseur()+"P"+id_produit);
        bandDeCommande.setmontant_tva(produit_fournisseur.getPrixUnitaire()*(produit_fournisseur.getTva()/100));
        bandDeCommande.setMontant_totale(bandDeCommande.getMontant()+bandDeCommande.getMontant_tva());
        bandDeCommande.setTitre("Proforma");
        bandDeCommande.setMode_payement(getDonnees.getAllMethodePayement());
        Fournisseur fournisseur = getDonnees.getFournisseurById(id_fournisseur);
        
        string subject = "Fournisseur : "+bandDeCommande.getProduitFournisseur().getFournisseur().getNomFournisseur()+" envoie un proforma";
        string textBody = "Nom produit : "+bandDeCommande.getProduitFournisseur().getProduit().getNomProduit()+" \n "+"Prix HT unitaire : "+bandDeCommande.getProduitFournisseur().getPrixUnitaire()+"Ariary \n "+"Quantite du produit : "+bandDeCommande.getQuantite()+" \n "+"Prix HT totale : "+bandDeCommande.getMontant()+" Ariary";
        string to = fournisseur.getEmail();
        IFormFile Attachment = null;
        if(HttpContext.Session.GetString("session").Equals("C1", StringComparison.OrdinalIgnoreCase)){
            Fournisseur.sendProformaEmail(subject,textBody,to,Attachment);
            return View("../band_de_commande/FormulaireBandDeCommande",bandDeCommande);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    public IActionResult CreerBandDeCommande(double quantite,string date,int id_produit,int id_fournisseur, int methode)
    {
            GetDonnees getDonnees = new GetDonnees();
            VStockProduitFournisseur produit_fournisseur = null;
            for(int i = 0; i < (getDonnees.getProformaByProduit(id_produit)).Count ; i++){
                if(getDonnees.getProformaByProduit(id_produit)[i].getFournisseur().getIdFournisseur() == id_fournisseur)
                produit_fournisseur = getDonnees.getProformaByProduit(id_produit)[i];
            }
            
            // alaina koa ny regroupement ny produit
            RegroupementBesoinModel regroupementbesoin = new RegroupementBesoinModel();
            List<VRegroupementBesoin> regroupement_besoin = getDonnees.getAllRegroupememtBesoin(id_produit);
            regroupementbesoin.setProduit(getDonnees.getProduitById(id_produit));
            regroupementbesoin.setRegroupementBesoin(regroupement_besoin);
            regroupementbesoin.setquantiteTotal(getDonnees.getQuantiteTotalDemandeBesoinByProduct(id_produit));

            BandDeCommandeModel bandDeCommande = new BandDeCommandeModel();
            bandDeCommande.setQuantite(quantite);
            bandDeCommande.setProduitFournisseur(produit_fournisseur);
            bandDeCommande.setRegroupementBesoin(regroupementbesoin);
            bandDeCommande.setMontant(produit_fournisseur.getPrixUnitaire()*quantite);
            bandDeCommande.setReference("F"+produit_fournisseur.getFournisseur().getIdFournisseur()+"P"+id_produit);
            bandDeCommande.setmontant_tva(produit_fournisseur.getPrixUnitaire()*(produit_fournisseur.getTva()/100));
            bandDeCommande.setMontant_totale(bandDeCommande.getMontant()+bandDeCommande.getMontant_tva());
            bandDeCommande.setDate(date);
            bandDeCommande.setTitre("bon de commande");
            bandDeCommande.setMode_payement(getDonnees.getAllMethodePayement());
            for(int hu = 0; hu < getDonnees.getAllMethodePayement().Count; hu++){
                if(getDonnees.getAllMethodePayement()[hu].getIDModePayement()==methode){
                    bandDeCommande.setMode_payementActive(getDonnees.getAllMethodePayement()[hu]);
                }
            }


            InsertDonnees insertDonne = new InsertDonnees();
            insertDonne.insertNewBandDeCommande(bandDeCommande,methode);

            if(quantite >= regroupementbesoin.getquantiteTotal()){
                // soloina terminer ny etat ny regroupement besoin sy demande besoin rehetra ao amin'ny regroupement besoin
                    insertDonne.updateEtatRegroupementBesoinToEnd(id_produit);
                    for(int i=0 ; i< regroupement_besoin.Count;i++){
                         insertDonne.updateEtatDemandeBesoinToEnd(regroupement_besoin[i].getDepartement().getIdDepartement(),regroupement_besoin[i].getProduit().getIdProduit());
                    }
                    // bandDeCommande.setQuantite(regroupementbesoin.getquantiteTotal());
                //
             }else{
                // ahena ny quantite ny regroupement besoin 
                //sy ny quantite demande besoin rehetra ao amin'ny regroupement besoin
                    // izay lasa efa nihena tonga de atao valider
                    // sinon atao moin
                // bandDeCommande.setQuantite(produit_fournisseur.getQuantite());
             }
        if(HttpContext.Session.GetString("session").Equals("C1", StringComparison.OrdinalIgnoreCase)){
            return View("../band_de_commande/bandDeCommandefin",bandDeCommande);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    
    public IActionResult ListeBandBandDeCommandeNonValide()
    {

        GetDonnees getDonnees = new GetDonnees();
        List<BandDeCommandeNonValideModel> bandDeCommande = getDonnees.getBonDeCommandeNonValider();
        // alaina daholo ny ao anatin ilay table
           // select id_fournisseur,id_fournisseur,date_de_commande,id_produit, sum(quantite) somme , id_methode_payement from bande_de_commande where etat = 5 group by id_fournisseur,id_produit,date_de_commande,id_methode_payement;
        if(HttpContext.Session.GetString("session").Equals("C2", StringComparison.OrdinalIgnoreCase)){
            return View("../band_de_commande/listebdnonval",bandDeCommande);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }

    public IActionResult ListeBandBandDeCommandeValide()
    {

        GetDonnees getDonnees = new GetDonnees();
        List<BandDeCommandeNonValideModel> bandDeCommande = getDonnees.getBonDeCommandeValider();
        // alaina daholo ny ao anatin ilay table
           // select id_fournisseur,id_fournisseur,date_de_commande,id_produit, sum(quantite) somme , id_methode_payement from bande_de_commande where etat = 5 group by id_fournisseur,id_produit,date_de_commande,id_methode_payement;
        if(HttpContext.Session.GetString("session").Equals("C2", StringComparison.OrdinalIgnoreCase)){
            return View("../band_de_commande/listebdval",bandDeCommande);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    public IActionResult validerBon(int id_fournisseur,string date)
    {
        if(HttpContext.Session.GetString("session").Equals("C2", StringComparison.OrdinalIgnoreCase)){
        InsertDonnees insertDonne = new InsertDonnees();
        insertDonne.updatebandDeCommandeToValide(id_fournisseur,date);
                GetDonnees getDonnees = new GetDonnees();

        List<BandDeCommandeNonValideModel> bandDeCommande = getDonnees.getBonDeCommandeNonValider();
        // alaina daholo ny ao anatin ilay table
           // select id_fournisseur,id_fournisseur,date_de_commande,id_produit, sum(quantite) somme , id_methode_payement from bande_de_commande where etat = 5 group by id_fournisseur,id_produit,date_de_commande,id_methode_payement;
            return View("../band_de_commande/listebdnonval",bandDeCommande);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    public IActionResult voirDetailbonDeCommande(int id_fournisseur,string date)
    {
        GetDonnees getDonnees = new GetDonnees();
        List<BandeDeCommandeDetail> bandDeCommande = getDonnees.GetBandeDeCommandeDetails();
        List<BandeDeCommandeDetail> bandDeCommandeVrai = new List<BandeDeCommandeDetail>();
        // List<BandDeCommandeNonValideModel> bandDeCommandes = getDonnees.getBonDeCommandeGros();
        double pu = 0;
        double tva = 0;
        double quantite =0;

        double ptht = 0;
        double pttva = 0;
        double ptat = 0;
        for(int i=0; i<bandDeCommande.Count ; i++){
            if(bandDeCommande[i].fournisseur.getIdFournisseur()==id_fournisseur && bandDeCommande[i].DateDeCommande == date && bandDeCommande[i].Etat == 5){
                pu = getDonnees.getPrixUnitaireProduitById(id_fournisseur,bandDeCommande[i].produit.getIdProduit());
                tva = bandDeCommande[i].produit.getTva();
                quantite = bandDeCommande[i].Quantite;
                bandDeCommande[i].prix_unitaire = pu;
                bandDeCommande[i].prix_ht =pu*quantite;
                bandDeCommande[i].prix_tva = pu*(tva/100);
                bandDeCommande[i].prix_total = (pu*quantite)+pu*(tva/100);

                ptht = ptht + (pu*quantite);
                pttva = pttva + (pu*(tva/100));
                ptat = ptat + ptht + pttva;

                bandDeCommande[i].prix_total_ht = ptht;
                bandDeCommande[i].prix_total_tva = pttva;
                bandDeCommande[i].prix_total_at = ptat;



                bandDeCommandeVrai.Add(bandDeCommande[i]);
            }
        }
                bandDeCommandeVrai[0].prix_total_ht = ptht;
                bandDeCommandeVrai[0].prix_total_tva = pttva;
                bandDeCommandeVrai[0].prix_total_at = ptat;
        // alaina daholo ny ao anatin ilay table
           // select id_fournisseur,id_fournisseur,date_de_commande,id_produit, sum(quantite) somme , id_methode_payement from bande_de_commande where etat = 5 group by id_fournisseur,id_produit,date_de_commande,id_methode_payement;
        if(HttpContext.Session.GetString("session").Equals("C2", StringComparison.OrdinalIgnoreCase)){
            return View("../band_de_commande/banddecommandedetails",bandDeCommandeVrai);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }
    
    public IActionResult voirDetailbonDeCommandeVal(int id_fournisseur,string date)
    {
        GetDonnees getDonnees = new GetDonnees();
        List<BandeDeCommandeDetail> bandDeCommande = getDonnees.GetBandeDeCommandeDetails();
        List<BandeDeCommandeDetail> bandDeCommandeVrai = new List<BandeDeCommandeDetail>();
        // List<BandDeCommandeNonValideModel> bandDeCommandes = getDonnees.getBonDeCommandeGros();
        double pu = 0;
        double tva = 0;
        double quantite =0;

        double ptht = 0;
        double pttva = 0;
        double ptat = 0;
        for(int i=0; i<bandDeCommande.Count ; i++){
            if(bandDeCommande[i].fournisseur.getIdFournisseur()==id_fournisseur && bandDeCommande[i].DateDeCommande == date && bandDeCommande[i].Etat == 10){
                pu = getDonnees.getPrixUnitaireProduitById(id_fournisseur,bandDeCommande[i].produit.getIdProduit());
                tva = bandDeCommande[i].produit.getTva();
                quantite = bandDeCommande[i].Quantite;
                bandDeCommande[i].prix_unitaire = pu;
                bandDeCommande[i].prix_ht =pu*quantite;
                bandDeCommande[i].prix_tva = pu*(tva/100);
                bandDeCommande[i].prix_total = (pu*quantite)+pu*(tva/100);

                ptht = ptht + (pu*quantite);
                pttva = pttva + (pu*(tva/100));
                ptat = ptat + ptht + pttva;

                bandDeCommande[i].prix_total_ht = ptht;
                bandDeCommande[i].prix_total_tva = pttva;
                bandDeCommande[i].prix_total_at = ptat;



                bandDeCommandeVrai.Add(bandDeCommande[i]);
            }
        }
                bandDeCommandeVrai[0].prix_total_ht = ptht;
                bandDeCommandeVrai[0].prix_total_tva = pttva;
                bandDeCommandeVrai[0].prix_total_at = ptat;
        // alaina daholo ny ao anatin ilay table
           // select id_fournisseur,id_fournisseur,date_de_commande,id_produit, sum(quantite) somme , id_methode_payement from bande_de_commande where etat = 5 group by id_fournisseur,id_produit,date_de_commande,id_methode_payement;

        if(HttpContext.Session.GetString("session").Equals("C2", StringComparison.OrdinalIgnoreCase)){
            return View("../band_de_commande/banddecommandedetails",bandDeCommandeVrai);
        }else{
            return View("../NotAccess/NoAccess");
        }
    }

}
