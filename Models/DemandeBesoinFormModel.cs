using Npgsql;
using System;
using tools;
namespace SystemeCommerciale
{
    public class DemandeBesoinFormModel
    {
        public List<Departement> departements { get; set; }
        public List<Produit> produits { get; set; }
    }
}
