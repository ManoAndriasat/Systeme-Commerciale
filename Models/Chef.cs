namespace tools;

public class Chef {
    public int idChef { get; set; }
    public string nomChef { get; set; }
    public string mdpChef { get; set; }

    public Chef(int idChef, string nomChef, string mdp) {
        this.idChef = idChef;
        this.nomChef = nomChef;
        this.mdpChef = mdp;
    }
}