namespace tools;

public class Departement {
    int idDepartement;
    string nomDepartement;

    public Departement(int idDepartement, string nomDepartement) {
        this.setIdDepartement(idDepartement);
        this.setNomDepartement(nomDepartement);
    }

    // getters
    public int getIdDepartement() {
        return this.idDepartement;
    }
    public string getNomDepartement() {
        return this.nomDepartement;
    }

    // setters
    public void setIdDepartement(int idDepartement) {
        this.idDepartement = idDepartement;
    }
    public void setNomDepartement(string nomDepartement) {
        this.nomDepartement = nomDepartement;
    }
}