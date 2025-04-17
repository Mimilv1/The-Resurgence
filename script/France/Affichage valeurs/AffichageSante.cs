using UnityEngine;
using UnityEngine.UI;

public class AffichageSante : MonoBehaviour
{
    // Script qui s'occupe d'afficher le slider qui s'occupe de la sante
    private Slider slide;
    public Image couleur;
    private float capacite_avant;
    private float besoin_avant;

    private void Start()
    {
        slide = GetComponent<Slider>();
        mise_a_jour(((float)VariablesEtMethodes.capacite_sante) / (VariablesEtMethodes.besoin_sante * 2 + 1));// +1 pour pas de dedivision par 0
    }
    public void mise_a_jour(float la_valeur)
    {
        if (VariablesEtMethodes.besoin_sante ==0 && VariablesEtMethodes.capacite_sante == 0)
        {
            slide.value = 0.5f;
            capacite_avant = VariablesEtMethodes.capacite_sante;
            besoin_avant = VariablesEtMethodes.besoin_sante;
            couleur.color = new Color32(57, 255, 20, 200);
        }
        else
        {
            slide.value = la_valeur;
            capacite_avant = VariablesEtMethodes.capacite_sante;
            besoin_avant = VariablesEtMethodes.besoin_sante;
            if (VariablesEtMethodes.capacite_sante < VariablesEtMethodes.besoin_sante)
            {
                couleur.color = new Color32(240, 0, 32, 200);
            }
            else
            {
                couleur.color = new Color32(57, 255, 20, 200);
            }
        }
    }
    private void Update()
    {
        if (besoin_avant != VariablesEtMethodes.besoin_sante || capacite_avant != VariablesEtMethodes.capacite_sante)
        {
            mise_a_jour(((float)VariablesEtMethodes.capacite_sante) / (VariablesEtMethodes.besoin_sante * 2 + 1));
        }
    }
}
