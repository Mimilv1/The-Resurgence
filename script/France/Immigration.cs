using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Immigration : MonoBehaviour
{
    // Script pour gerer l'achat de population
    private TextMeshProUGUI prix;
    private int le_prix;
    private float valeur;
    private int la_pop;
    private float valeur_diff;
    public TextMeshProUGUI pop;
    public Slider le_slide;
    void Start()
    {
        prix = gameObject.GetComponent<TextMeshProUGUI>();
        valeur = le_slide.value;
        valeur_diff = 0;
        mise_a_jour();
    }
    private void OnEnable()
    {
        prix = gameObject.GetComponent<TextMeshProUGUI>();
        mise_a_jour();
    }

    void Update()
    {
        valeur = le_slide.value;
        if (valeur != valeur_diff)
        {
            mise_a_jour();
        }
    }
    public void achat()
    {
        if (VariablesEtMethodes.argent >= le_prix)
        {
            VariablesEtMethodes.argent -= le_prix;
            VariablesEtMethodes.population += la_pop;
            VariablesEtMethodes.pop_acheter = la_pop;
            VariablesEtMethodes.immigration_cour += Random.Range(1, 10);
            mise_a_jour();
        }
    }
    private void mise_a_jour()
    {
        la_pop = (int)(valeur * VariablesEtMethodes.argent / VariablesEtMethodes.immigration_cour);
        le_prix = VariablesEtMethodes.immigration_cour * la_pop;
        prix.text = "Prix : " + le_prix.ToString();
        pop.text = "Population : " + la_pop.ToString();
        valeur_diff = valeur;
    }
}
