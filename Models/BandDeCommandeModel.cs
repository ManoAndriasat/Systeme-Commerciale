using Npgsql;
using System;
using tools;
namespace SystemeCommerciale;
    public class BandDeCommandeModel
    {
        public VStockProduitFournisseur produit_fournisseur;
        public RegroupementBesoinModel regroupementbesoin;
        public string reference;
        public double montant_totale;
        public double montant_tva;
        public double montant;
        public double quantite;
        public string date;
        public string titre;
        public List<ModePayement> mode_payement;
        public ModePayement mode_payementActive;

        public void setMode_payement(List<ModePayement> m){
                mode_payement = m; 
        }
        public List<ModePayement> getMode_payement(){
            return mode_payement;
        }
        public void setMode_payementActive(ModePayement m){
                mode_payementActive = m;
        }
        public ModePayement getMode_payementActive(){
            return mode_payementActive;
        }

        public void setTitre(string t){
            titre = t;
        }
        public string getTitre(){
            return titre;
        }

        public void setDate(string d){
            date = d;
        }
        public string getDate(){
            return date;
        }

        public double getQuantite(){
            return quantite;
        }
        public void setQuantite(double q){
            quantite = q;
        }

        public double getMontant_tva(){
            return montant_tva;
        }
        public double getMontant(){
            return montant;
        }
        public void setmontant_tva(double m){
             montant_tva = m;
        }
        public void setMontant(double m){
             montant = m;
        }

        public void setReference(String r){
            reference = r;
        }
        public void setMontant_totale(double mon){
            montant_totale = mon;
        }
        
        public void setProduitFournisseur(VStockProduitFournisseur v){
            produit_fournisseur = v;
        }
        public VStockProduitFournisseur getProduitFournisseur(){
            return produit_fournisseur;
        }
        public void setRegroupementBesoin(RegroupementBesoinModel v){
            regroupementbesoin = v;
        }
        public RegroupementBesoinModel getRegroupementBesoin(){
            return regroupementbesoin;
        }
        public string getReference(){
            return reference;
        }
        public double getMontant_totale(){
            return montant_totale;
        }

        public BandDeCommandeModel(){}
    }
