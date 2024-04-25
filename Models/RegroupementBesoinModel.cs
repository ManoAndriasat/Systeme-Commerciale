using Npgsql;
using System;
using tools;
namespace SystemeCommerciale
{
    public class RegroupementBesoinModel
    {
        public Produit produit;
        public double quantiteTotal;
        public List<VRegroupementBesoin> regroupement_besoin;
        // setters
        public void setProduit(Produit p){
            produit = p;
        }
        public void setquantiteTotal(double quantite){
            quantiteTotal = quantite;
        }
        public void setRegroupementBesoin(List<VRegroupementBesoin> regroupement){
            regroupement_besoin = regroupement;
        }
        public double getquantiteTotal(){
            return quantiteTotal;
        }
        public List<VRegroupementBesoin> getRegroupementBesoin(){
            return regroupement_besoin;
        }
        public Produit getProduit(){
            return produit;
        }

        public RegroupementBesoinModel(){}
    }
}
