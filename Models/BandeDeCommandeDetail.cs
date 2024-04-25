namespace SystemeCommerciale;
using tools;
public class BandeDeCommandeDetail
{
    public string IdBandeDeCommande { get; set; }
    public string Titre { get; set; }
    public Fournisseur fournisseur { get; set; }
    public string DateDeCommande { get; set; }
    public Produit produit { get; set; }
    public double Quantite { get; set; }
    public int Etat { get; set; }
    public ModePayement mode_payement { get; set; }
    public double tva { get;set;}
    public double prix_unitaire {get;set;}
    public double prix_ht {get;set;}
    public double prix_tva {get;set;}
    public double prix_total{get;set;}

    public double prix_total_ht {get;set;}
    public double prix_total_tva {get;set;}
    public double prix_total_at {get;set;}
}
