using UnityEngine;
using UnityEngine.UI;

public class AffichageElec : MonoBehaviour
{
    // Script qui s'occupe du slider indiquant l'electricite
    private Slider slide ;
    public Image couleur;
    private float production_avant;
    private float demande_avant;
    private void Start()
    {
        production_avant = VariablesEtMethodes.production_electrique;
        demande_avant = VariablesEtMethodes.demande_electrique;
        slide = GetComponent<Slider>();
        mise_a_jour(((float)VariablesEtMethodes.production_electrique) / (VariablesEtMethodes.demande_electrique * 2 + 1));// +1 pour pas de dedivision par 0
    }
    public void mise_a_jour(float la_valeur)
    {
        if (VariablesEtMethodes.demande_electrique == 0 && VariablesEtMethodes.production_electrique == 0)
        {
            slide.value = 0.5f;
            production_avant = VariablesEtMethodes.production_electrique;
            demande_avant = VariablesEtMethodes.demande_electrique;
            couleur.color = new Color32(57, 255, 20, 200);
        }
        else
        {
            slide.value = la_valeur;
            production_avant = VariablesEtMethodes.production_electrique;
            demande_avant = VariablesEtMethodes.demande_electrique;
            if (VariablesEtMethodes.production_electrique < VariablesEtMethodes.demande_electrique)
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
        if (production_avant != VariablesEtMethodes.production_electrique || demande_avant!=VariablesEtMethodes.demande_electrique)
        {
            mise_a_jour(((float)VariablesEtMethodes.production_electrique) / (VariablesEtMethodes.demande_electrique * 2 + 1));
        }
    }
}
