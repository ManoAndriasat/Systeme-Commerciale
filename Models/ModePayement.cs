using connex;

namespace tools;

public class ModePayement {
    int id_mode_payement;
    string designation;
    

    // getters
    public int getIDModePayement() {
        return id_mode_payement;
    }
    public string getdesignation() {
        return this.designation;
    }


    // setters
    public void setIDModePayement(int id) {
        this.id_mode_payement = id;
    }
    public void setdesignation(string des) {
        this.designation = des;
    }


}