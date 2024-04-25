namespace tools;

public class DemandeBesoin {
    Departement departement;
    Produit produit;
    double quantite;
    int etat;
    int id_demande_besoin;

    public DemandeBesoin(Departement departement, Produit produit, double quantite, int etat) {
        this.setDepartement(departement);
        this.setProduit(produit);
        this.setQuantite(quantite);
        this.setEtat(etat);
    }

    // getters
    public Departement getDepartement() {
        return this.departement;
    }
    public Produit getProduit() {
        return this.produit;
    }
    public double getQuantite() {
        return this.quantite;
    }
    public int getEtat() {
        return this.etat;
    }
    public int getId_demande_besoin(){
        return this.id_demande_besoin;
    }

    // setters
    public void setDepartement(Departement departement) {
        this.departement = departement;
    }
    public void setProduit(Produit produit) {
        this.produit = produit;
    }
    public void setQuantite(double quantite) {
        this.quantite = quantite;
    }
    public void setEtat(int etat) {
        this.etat = etat;
    }
    public void setId_demande_besoin(int id_demande_besoin) {
        this.id_demande_besoin = id_demande_besoin;
        // Console.WriteLine();
    }
}