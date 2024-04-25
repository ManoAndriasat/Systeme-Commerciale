using Npgsql;

namespace connex;

public class ConnectPostgres {
    public NpgsqlConnection GetConnex() {
        string connectionString = "Host=localhost;Port=5432;Database=systeme_commerciale;Username=postgres;Password=Mano-123;";
        NpgsqlConnection connexion = new NpgsqlConnection(connectionString);

        return connexion;
    }
}