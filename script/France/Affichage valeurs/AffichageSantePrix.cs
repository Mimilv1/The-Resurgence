using UnityEngine;
using TMPro;

public class AffichageSantePrix : MonoBehaviour
{
    // Calcul et affiche le prix de la sante sur le marche internationale
    private TextMeshProUGUI textMesh;
    private bool achat_possible;
    void OnEnable()
    {
        int valeur;
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        valeur = VariablesEtMethodes.besoin_sante - VariablesEtMethodes.capacite_sante;
        if (valeur > 0)
        {
            achat_possible = true;
            int prix;
            prix = valeur * VariablesEtMethodes.cour_sante;
            textMesh.text = "Le prix est de " + (prix.ToString()) + " pour repondre a la demande de sante pour ce tour";
        }
        else
        {
            achat_possible = false;
            textMesh.text = "Capacite sanitaire suffisante";
        }
    }

    public void acheter()
    {
        if (achat_possible)
        {
            int prix = (VariablesEtMethodes.besoin_sante - VariablesEtMethodes.capacite_sante) * VariablesEtMethodes.cour_sante;
            if (VariablesEtMethodes.argent >= prix)
            {
                VariablesEtMethodes.argent -= prix;
                VariablesEtMethodes.sante_acheter += VariablesEtMethodes.besoin_sante - VariablesEtMethodes.capacite_sante;
                VariablesEtMethodes.calcul_bat();
                VariablesEtMethodes.cour_sante += Random.Range(1, 3);
                textMesh.text = "Capacite sanitaire suffisante";
            }
        }
    }
}
