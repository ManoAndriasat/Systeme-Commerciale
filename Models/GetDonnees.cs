using Npgsql;
using tools;
using SystemeCommerciale;
namespace connex;

public class GetDonnees {
    ConnectPostgres connectPostgres = new ConnectPostgres();

    // get all methode payement
      public List<ModePayement> getAllMethodePayement() {
        List<ModePayement> allmode = new List<ModePayement>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = "select * from methode_de_payement";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        ModePayement mode = new ModePayement();
                        mode.setIDModePayement(reader.GetInt32(0));
                        mode.setdesignation(reader.GetString(1));
                        allmode.Add(mode);
                    }
                }
            }
        }

        return allmode;
    }

    public ModePayement getMethodePayementById(int id) {
        ModePayement mode = null;
        List<ModePayement> modes = getAllMethodePayement();

        for(int i = 0; i < modes.Count ; i++){
            if(modes[i].getIDModePayement()==id){
                mode = modes[i];
            }
        }

        return mode;
    }

    // get all departement
    public List<Departement> getAllDepartement() {
        List<Departement> allDepartement = new List<Departement>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = "SELECT * FROM departement";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        Departement departement = new Departement(0, "");
                        departement.setIdDepartement(reader.GetInt32(0));
                        departement.setNomDepartement(reader.GetString(1));
                        allDepartement.Add(departement);
                    }
                }
            }
        }

        return allDepartement;
    }
    // get all chef
    public List<Chef> getAllChef() {
        List<Chef> allChef = new List<Chef>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = "SELECT * FROM chef";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        Chef chef = new Chef(0, "", "");
                        chef.idChef = reader.GetInt32(0);
                        chef.nomChef = reader.GetString(1);
                        chef.mdpChef = reader.GetString(1);

                        allChef.Add(chef);
                    }
                }
            }
        }

        return allChef;
    }

    // get departement by id
    public Departement getDepartementById(int id) {
        Departement departement = new Departement(0, "");
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"SELECT * FROM departement where id_departement = {id}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        departement.setIdDepartement(reader.GetInt32(0));
                        departement.setNomDepartement(reader.GetString(1));
                    }
                }
            }
        }

        return departement;
    }

    // get fournisseur by id
        public Fournisseur getFournisseurById(int id) {
        Fournisseur fournisseur = new Fournisseur();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"select * from fournisseur where id_fournisseur = {id}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        fournisseur.setNomFournisseur(reader.GetString(0));
                        fournisseur.setContact(reader.GetString(1));
                        fournisseur.setEmail(reader.GetString(2));
                        fournisseur.setIdFournisseur(reader.GetInt32(3));
                    }
                }
            }
        }

        return fournisseur;
    }

    // get all produit
    public List<Produit> getAllProduit() {
        List<Produit> allproduit = new List<Produit>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = "SELECT * FROM produit";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        Produit produit = new Produit(reader.GetInt32(0),reader.GetString(1),reader.GetDouble(2));
                        allproduit.Add(produit);
                    }
                }
            }
        }

        return allproduit;
    }

    // get produit by id
       public Produit getProduitById(int id_produit) {
        Produit produit = null;
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"SELECT * FROM produit where id_produit = {id_produit}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        produit = new Produit(reader.GetInt32(0),reader.GetString(1),reader.GetDouble(2));
                    }
                }
            }
        }
        return produit;
    }

    // get demande non valide
    public List<DemandeBesoin> getAllDemandeNonVal() {
        List<DemandeBesoin> allDemandeNonVal = new List<DemandeBesoin>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = "SELECT * FROM v_demande_besoin WHERE etat = 5";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        //id_departement |     nom_departement      | id_produit | nom_produit | tva | quantite | etat
                        DemandeBesoin demandeBesoin = new DemandeBesoin(null, null, 0, 0);
                        demandeBesoin.setDepartement(new Departement(reader.GetInt32(0), reader.GetString(1)));
                        demandeBesoin.setProduit(new Produit(reader.GetInt32(2), reader.GetString(3), reader.GetDouble(4)));
                        demandeBesoin.setQuantite(reader.GetDouble(5));
                        demandeBesoin.setEtat(reader.GetInt32(6));
                        demandeBesoin.setId_demande_besoin(reader.GetInt32(7));
                        allDemandeNonVal.Add(demandeBesoin);
                    }
                }
            }
        }

        return allDemandeNonVal;
    }

    public DemandeBesoin getDemandeNonValById(int id_demande_besoin) {
        DemandeBesoin demandeBesoin = null;
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"SELECT * FROM v_demande_besoin WHERE etat = 5 and id_demande_besoin = {id_demande_besoin}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        //id_departement |     nom_departement      | id_produit | nom_produit | tva | quantite | etat
                        demandeBesoin = new DemandeBesoin(null, null, 0, 0);
                        demandeBesoin.setDepartement(new Departement(reader.GetInt32(0), reader.GetString(1)));
                        demandeBesoin.setProduit(new Produit(reader.GetInt32(2), reader.GetString(3), reader.GetDouble(4)));
                        demandeBesoin.setQuantite(reader.GetDouble(5));
                        demandeBesoin.setEtat(reader.GetInt32(6));
                        demandeBesoin.setId_demande_besoin(reader.GetInt32(7));
                    }
                }
            }
        }

        return demandeBesoin;
    }

    // get all regroupement besoin
    public List<VRegroupementBesoin> getAllRegroupememtBesoin(int id){
        List<VRegroupementBesoin> allregroupementBesoin = new List<VRegroupementBesoin>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"select * from v_regroupement_besoin where id_produit={id} and etat_regroupement_besoin=5 order by id_regroupement_besoin asc";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        VRegroupementBesoin regroupement_besoin = new VRegroupementBesoin();
                        regroupement_besoin.setDepartement(getDepartementById(reader.GetInt32(0)));
                        regroupement_besoin.setProduit(getProduitById(reader.GetInt32(1)));
                        regroupement_besoin.setQuantite(reader.GetDouble(2));
                        allregroupementBesoin.Add(regroupement_besoin);
                    }
                }
            }
        }

        return allregroupementBesoin;
    }

    public double getQuantiteTotalDemandeBesoinByProduct(int id){
        double quantite = 0;
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"select sum(quantite) somme from v_regroupement_besoin where id_produit = {id} and etat_demande_besoin = 10";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        quantite = reader.GetDouble(0);
                    }
                }
            }
        }

        return quantite;
    }

    public List<VStockProduitFournisseur> getProformaByProduit(int id){
        List<VStockProduitFournisseur> proformas = new List<VStockProduitFournisseur>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"select * from v_stock_produit_fournisseur where id_produit={id} order by prix_unitaire asc limit 3";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        VStockProduitFournisseur proforma = new VStockProduitFournisseur();
                        proforma.setProduit(getProduitById(id));
                        proforma.setTva((reader.GetDouble(2)));
                        proforma.setPrixUnitaire(reader.GetDouble(4));
                        proforma.setFournisseur(getFournisseurById(reader.GetInt32(3)));
                        proforma.setQuantite(reader.GetDouble(5));
                        proformas.Add(proforma);
                    }
                }
            }
        }

        return proformas;
    }

    
        public List<BandDeCommandeNonValideModel> getBonDeCommandeNonValider(){
        List<BandDeCommandeNonValideModel> bons = new List<BandDeCommandeNonValideModel>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"SELECt bd.id_fournisseur, bd.date_de_commande,bd.id_methode_payement,SUM(sp.quantite * sp.prix_unitaire) AS prix_total FROM    bande_de_commande bd JOIN stock_produit sp ON bd.id_produit = sp.id_produit AND bd.id_fournisseur = sp.id_fournisseur WHERE bd.etat = 5 GROUP By bd.id_fournisseur,bd.date_de_commande,   bd.id_methode_payement";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        BandDeCommandeNonValideModel bon = new BandDeCommandeNonValideModel();
                        bon.fournisseur = getFournisseurById(reader.GetInt32(0));
                        bon.DateDeCommande = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        bon.IdMethodePayement = reader.GetInt32(2);
                        bon.PrixTotal = reader.GetDouble(3);
                        bons.Add(bon);
                    }
                }
            }
        }

        return bons;
    }

    public List<BandDeCommandeNonValideModel> getBonDeCommandeValider(){
        List<BandDeCommandeNonValideModel> bons = new List<BandDeCommandeNonValideModel>();
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"SELECt bd.id_fournisseur, bd.date_de_commande,bd.id_methode_payement,SUM(sp.quantite * sp.prix_unitaire) AS prix_total FROM    bande_de_commande bd JOIN stock_produit sp ON bd.id_produit = sp.id_produit AND bd.id_fournisseur = sp.id_fournisseur WHERE bd.etat = 10 GROUP By bd.id_fournisseur,bd.date_de_commande,   bd.id_methode_payement";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        BandDeCommandeNonValideModel bon = new BandDeCommandeNonValideModel();
                        bon.fournisseur = getFournisseurById(reader.GetInt32(0));
                        bon.DateDeCommande = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        bon.IdMethodePayement = reader.GetInt32(2);
                        bon.PrixTotal = reader.GetDouble(3);
                        bons.Add(bon);
                    }
                }
            }
        }

        return bons;
    }
    public List<BandeDeCommandeDetail> GetBandeDeCommandeDetails()
    {
        List<BandeDeCommandeDetail> bandeDeCommandeDetails = new List<BandeDeCommandeDetail>();

        using (NpgsqlConnection connex = connectPostgres.GetConnex())
        {
            connex.Open();

            string query = "SELECT id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement FROM bande_de_commande";
            
            using (NpgsqlCommand command = new NpgsqlCommand(query, connex))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BandeDeCommandeDetail bandeDeCommandeDetail = new BandeDeCommandeDetail();
                        
                            bandeDeCommandeDetail.IdBandeDeCommande = "BD00"+reader.GetInt32(0);
                            bandeDeCommandeDetail.Titre = reader.GetString(1);
                            bandeDeCommandeDetail.fournisseur = getFournisseurById(reader.GetInt32(2));
                            bandeDeCommandeDetail.DateDeCommande = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                            bandeDeCommandeDetail.produit = getProduitById(reader.GetInt32(4));
                            bandeDeCommandeDetail.Quantite = reader.GetDouble(5);
                            bandeDeCommandeDetail.Etat = reader.GetInt32(6);
                            bandeDeCommandeDetail.mode_payement = getMethodePayementById(reader.GetInt32(7));
                        

                        bandeDeCommandeDetails.Add(bandeDeCommandeDetail);
                    }
                }
            }
        }

        return bandeDeCommandeDetails;
    }
    public double getPrixUnitaireProduitById(int id_fournisseur,int id_produit){
        double quantite = 0;
        NpgsqlConnection connex = this.connectPostgres.GetConnex();
        using(connex) {
            connex.Open();
            string query = $"select prix_unitaire from stock_produit where id_fournisseur = {id_fournisseur} and id_produit = {id_produit}";
            using(NpgsqlCommand command = new NpgsqlCommand(query, connex)) {
                using(NpgsqlDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        quantite = reader.GetDouble(0);
                    }
                }
            }
        }

        return quantite;
    }
    
}