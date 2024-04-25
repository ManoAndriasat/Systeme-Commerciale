using connex;

namespace tools;

public class Produit {
    int idProduit;
    string nomProduit;
    double tva;

    public Produit(int idProduit, string nomProduit, double tva) {
        this.setIdproduit(idProduit);
        this.setNomProduit(nomProduit);
        this.setTva(tva);
    }

    // getters
    public int getIdProduit() {
        return this.idProduit;
    }
    public string getNomProduit() {
        return this.nomProduit;
    }
    public double getTva() {
        return this.tva;
    }

    // setters
    public void setIdproduit(int idProduit) {
        this.idProduit = idProduit;
    }
    public void setNomProduit(string nomProduit) {
        this.nomProduit = nomProduit;
    }
    public void setTva(double tva) {
        this.tva = tva;
    }

    // insert noveau produit
    public void insertProduit() {
        InsertDonnees insertDonnees = new InsertDonnees();
        insertDonnees.insertProduit(this);
    }

}