using Npgsql;
using System;
using tools;
namespace SystemeCommerciale
{
    public class ProformaModel
    {
        public Produit produit;
        public List<VStockProduitFournisseur> v_stock_produit_fournisseur;
        // setters
        public void setProduit(Produit p){
            produit = p;
        }
        
        public void setVStockProduitFournisseur(List<VStockProduitFournisseur> v){
            v_stock_produit_fournisseur = v;
        }
        public List<VStockProduitFournisseur> getVStockProduitFournisseur(){
            return v_stock_produit_fournisseur;
        }
        public Produit getProduit(){
            return produit;
        }

        public ProformaModel(){}
    }
}
