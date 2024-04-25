 namespace tools;

public class VRegroupementBesoin {
    Departement departement;
    Produit produit;
    double quantite;
    
    // setters
    public void setDepartement(Departement dept){
        departement = dept;
    }
    public void setProduit(Produit pro){
        produit = pro;
    }
    public void setQuantite(double q){
        quantite = q;
    }
    
    // getters
    public Departement getDepartement(){
        return departement;
    }
    public Produit getProduit(){
        return produit;
    }
    public double getQuantite(){
        return quantite;
    }

    public VRegroupementBesoin(){

    }

}