using UnityEngine;
using TMPro;

public class AffichageElecPrix : MonoBehaviour
{
    // Script qui calcule et affiche le prix pour stabiliser la demande electrique
    private TextMeshProUGUI textMesh;
    private bool achat_possible;
    void OnEnable()
    {
        int valeur;
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        valeur = VariablesEtMethodes.demande_electrique - VariablesEtMethodes.production_electrique;
        if (valeur > 0)
        {
            achat_possible = true;
            int prix;
            prix = valeur * VariablesEtMethodes.cour_elec;
            textMesh.text = "Le prix est de " + (prix.ToString()) + " pour stabiliser la demande electrique pour le moment";
        }
        else
        {
            achat_possible = false;
            textMesh.text = "Production electrique suffisante";
        }
    }

    public void acheter() 
    {
        if (achat_possible)
        { 
            int prix = (VariablesEtMethodes.demande_electrique - VariablesEtMethodes.production_electrique) * VariablesEtMethodes.cour_elec;
            if (VariablesEtMethodes.argent >= prix)
            {
                VariablesEtMethodes.argent -= prix;
                VariablesEtMethodes.elec_acheter += (VariablesEtMethodes.demande_electrique - VariablesEtMethodes.production_electrique);
                VariablesEtMethodes.calcul_bat();
                VariablesEtMethodes.cour_elec += Random.Range(10, 30);
                textMesh.text = "Production electrique suffisante";
            }
        }
    }
}
