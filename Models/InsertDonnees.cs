using Npgsql;
using tools;
using SystemeCommerciale;
namespace connex;

public class InsertDonnees {
    ConnectPostgres connectPostgres = new ConnectPostgres();

    // insert produit
    public void insertProduit(Produit produits) {
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"INSERT INTO produit VALUES (DEFAULT, '{produits.getNomProduit()}', {produits.getTva()})";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void insertDemandeBesoin(int departement,int produit,double quantite) {
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"INSERT INTO demande_besoin(id_departement,id_produit,quantite,etat) VALUES ({departement},{produit}, {quantite},5)";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void updateEtatDemandeBesoinToValide(DemandeBesoin demandebesoin) {
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"update demande_besoin set etat = 10 where id_demande_besoin= { demandebesoin.getId_demande_besoin()}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void updateEtatDemandeBesoinToNonValide(DemandeBesoin demandebesoin) {
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"update demande_besoin set etat = 0 where id_demande_besoin= { demandebesoin.getId_demande_besoin()}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void insertToRegroupementBesoin(DemandeBesoin demandebesoin) {
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
             
            string query = $"insert into regroupement_besoin( id_produit,quantite,etat,id_demande_besoin) values ({demandebesoin.getProduit().getIdProduit()},{demandebesoin.getQuantite()},{5},{demandebesoin.getId_demande_besoin()})";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void insertNewBandDeCommande(BandDeCommandeModel bandDeCommande,int methode){
        string titre = bandDeCommande.getTitre();
        int id_fournisseur = bandDeCommande.getProduitFournisseur().getFournisseur().getIdFournisseur();
        string date = bandDeCommande.getDate();
        int id_produit =  bandDeCommande.getProduitFournisseur().getProduit().getIdProduit();
        double quantite = bandDeCommande.getQuantite();
        int etat = 5;
        String querys = "INSERT INTO bande_de_commande(titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement) VALUES ('"+titre+"',"+id_fournisseur+",'"+date+"',"+id_produit+" ,"+quantite+" ,"+etat+", "+methode+")";
    
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = querys;
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void updateEtatRegroupementBesoinToEnd(int id_produit){
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"update regroupement_besoin set etat = 10 where id_produit= { id_produit }";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    public void updateEtatDemandeBesoinToEnd(int id_departement,int id_produit){
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"update demande_besoin set etat = 10 where id_produit= { id_produit } and id_departement = { id_departement }";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }

    public void updatebandDeCommandeToValide(int id_fournisseur,string date){
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"update bande_de_commande set etat = 10 where date_de_commande= '{ date }' and id_fournisseur = { id_fournisseur }";
            Console.WriteLine(id_fournisseur+" "+date);
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                command.ExecuteNonQuery();
            }
        
        }
    }
    
    
}