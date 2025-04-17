using UnityEngine;

public class ToutesLesRecherches : MonoBehaviour
{
    // Contient les différentes recherche qui sont activer quand le joueur achète une recherche
    public static void recherche_pop_max(int pourcentage)
    {
        VariablesEtMethodes.pourcentage_max_population += (float)pourcentage/100;
        VariablesEtMethodes.Affichage_update();
    }
    public static void recherche_pop_gain(int pourcentage)
    {
        VariablesEtMethodes.pourcentage_augmente_pop += (float)pourcentage/100;
        VariablesEtMethodes.Affichage_update();
    }
    public static void recherche_batiment(int id) // 0 centrale nucleaire, 1 charbon, 2 lycee, 3 universite, 4 maison sante, 5 hopital, 6 ville n2, 7 ville n3
    {
        VariablesEtMethodes.recherche[id] = true;
    }
    public static void recherche_changement_nuke(int proba)
    {
        VariablesEtMethodes.chance_nuke = proba;
    }
    public static void recherche_prix_charbon(float valeur)
    {
        VariablesEtMethodes.pourcentage_prix_du_charbon = valeur;
    }
    public static void recherche_eolienne_prod(float valeur)
    {
        VariablesEtMethodes.pourcentage_production_eolienne = valeur;
    }
    public static void recherche_capacite_sante(float valeur)
    {
        VariablesEtMethodes.pourcentage_capacite_sante += valeur / 100;
    }
    public static void recherche_demande_sante(float valeur)
    {
        VariablesEtMethodes.pourcentage_besoin_sante -= valeur / 100;
    }
    public static void recherche_reduction_sante(float valeur)
    {
        VariablesEtMethodes.prix_clinique = (int)(VariablesEtMethodes.prix_clinique * (1-valeur));
        VariablesEtMethodes.prix_hopital = (int)(VariablesEtMethodes.prix_hopital * (1-valeur));
        VariablesEtMethodes.prix_maison_sante = (int)(VariablesEtMethodes.prix_maison_sante * (1-valeur));
    }
    public static void recherche_pourcentage_education(float valeur)
    {
        VariablesEtMethodes.pourcentage_taux_education += valeur;
    }
    public static void recherche_prix_education(float valeur)
    {
        VariablesEtMethodes.prix_ecole = (int)(VariablesEtMethodes.prix_ecole * (1 - valeur));
        VariablesEtMethodes.prix_lycee = (int)(VariablesEtMethodes.prix_lycee * (1 - valeur));
        VariablesEtMethodes.prix_universite = (int)(VariablesEtMethodes.prix_universite * (1 - valeur));
    }
}
