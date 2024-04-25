using Npgsql;
using System;
using tools;
namespace SystemeCommerciale
{
    public class VStockProduitFournisseur
    {
        //id_produit | nom_produit  | tva | id_fournisseur | prix_unitaire | nom_fournisseur |  contact   |          email
        public Produit produit;
        double tva;
        double prix_unitaire;
        double quantite;
        Fournisseur fournisseur;
        // setters
        public void setQuantite(double q){
            quantite = q;
        }
        public void setProduit(Produit p){
            produit = p;
        }
        public void setTva(double tva){
            this.tva = tva;
        }
        public void setPrixUnitaire(double p){
            prix_unitaire = p;
        }
        public void setFournisseur(Fournisseur f){
            fournisseur = f;
        }
        
        // getters
        public Produit getProduit(){
            return produit;
        }
        public double getTva(){
            return tva;
        }
        public double getPrixUnitaire(){
            return prix_unitaire;
        }
        public Fournisseur getFournisseur(){
            return fournisseur;
        }
        public double getQuantite(){
            return quantite;
        }

        public VStockProduitFournisseur(){}
    }
}
