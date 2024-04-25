var date = document.getElementById("date");
var quantite = document.getElementById("quantite");
var lien = document.getElementById("lien");
var lienhref = lien.getAttribute("href");
var id_produit = document.getElementById("id_produit").value;
var id_fournisseur = document.getElementById("id_fournisseur").value;


var pu = document.getElementById("pu").innerText;
var tva = document.getElementById("tva").innerText;
var montant = document.getElementById("montant");
var montant_2 = document.getElementById("montant_2");
var montant_tva = document.getElementById("montant_tva");
var montant_totale = document.getElementById("montant_totale");
var methode = document.getElementById("methode");

date.addEventListener("input", createLien);
quantite.addEventListener("input",createLien);
methode.addEventListener("change",createLien);


function createLien()
{
    var pum =pu;
    var methodem = methode.value;
    var quantitem =quantite.value;
    var montantm = pu*quantitem;
    var mtva = montantm*(tva/100);
    var montant_totalem = montantm+mtva;
    montant.textContent = montantm;
    montant_2.textContent = montantm;
    montant_tva.textContent = mtva;
    montant_totale.textContent = montant_totalem;
    lien.setAttribute("href","CreerBandDeCommande/?quantite="+quantite.value+"&date="+date.value+"&id_produit="+id_produit+"&id_fournisseur="+id_fournisseur+"&methode="+methodem);
}