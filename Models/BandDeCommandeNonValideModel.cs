namespace SystemeCommerciale;
using tools;

public class BandDeCommandeNonValideModel
{
    public int IdBandeDeCommande { get; set; }
    public Fournisseur fournisseur { get; set; }
    public int IdProduit { get; set; }
    public string DateDeCommande { get; set; }
    public int IdMethodePayement { get; set; }
    public double SommeQuantite { get; set; }
    public double PrixTotal { get; set; }

}
