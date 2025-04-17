using UnityEngine;
using TMPro;

public class AfficherText : MonoBehaviour
{
    // Affichage des Prix des contructions
    public Color couleur;
    public int id_prix; // 0 prix ville, 1 prix eolienne, 2 prix centrale charbon, 3 prix centrale nucléaire, 4 clinique, 5 maison sante, 6 hopital, 7 ecole, 8 lycee, 9 universite, 10 vill_lv2, 11 ville_lv3
    public TextMeshProUGUI texte; // on fait pas un getcomponent car il est pas actif de base
    public GameObject le_texte;
    private int[] ma_liste = { VariablesEtMethodes.prix_ville, VariablesEtMethodes.prix_eolienne, VariablesEtMethodes.prix_usine_charbon, VariablesEtMethodes.prix_nuke,
        VariablesEtMethodes.prix_clinique, VariablesEtMethodes.prix_maison_sante, VariablesEtMethodes.prix_hopital, VariablesEtMethodes.prix_ecole, VariablesEtMethodes.prix_lycee,
        VariablesEtMethodes.prix_universite, 0, 0};
    void Start()
    {
        ma_liste[10] = VariablesEtMethodes.prix_ville + gameObject.GetComponentInParent<BuildingButton>().ville.GetComponentInChildren<ScriptBatiment>().prix_upgrade;
        ma_liste[11] = ma_liste[10] + gameObject.GetComponentInParent<BuildingButton>().ville.GetComponentInChildren<ScriptBatiment>().prix_up[0];
        texte.text = ma_liste[id_prix].ToString();
        texte.color = couleur;
        Physics.queriesHitTriggers = true;
    }
    private void OnMouseEnter()
    {
        le_texte.SetActive(true);
    }
    private void OnMouseExit()
    {
        le_texte.SetActive(false);
    }
    void OnEnable()
    {
        ma_liste = new int[]{
        VariablesEtMethodes.prix_ville, VariablesEtMethodes.prix_eolienne, VariablesEtMethodes.prix_usine_charbon, VariablesEtMethodes.prix_nuke,
        VariablesEtMethodes.prix_clinique, VariablesEtMethodes.prix_maison_sante, VariablesEtMethodes.prix_hopital, VariablesEtMethodes.prix_ecole,VariablesEtMethodes.prix_lycee,
        VariablesEtMethodes.prix_universite, VariablesEtMethodes.prix_ville + gameObject.GetComponentInParent<BuildingButton>().ville.GetComponentInChildren<ScriptBatiment>().prix_upgrade,
        VariablesEtMethodes.prix_ville + gameObject.GetComponentInParent<BuildingButton>().ville.GetComponentInChildren<ScriptBatiment>().prix_upgrade + gameObject.GetComponentInParent<BuildingButton>().ville.GetComponentInChildren<ScriptBatiment>().prix_up[0]};
        
        texte.text = ma_liste[id_prix].ToString();
    }
}
