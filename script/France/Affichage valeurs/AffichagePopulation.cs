using UnityEngine;
using TMPro;

public class AffichagePopulation : MonoBehaviour
{
    // Script qui affiche le nombre d'habitant
    private TextMeshProUGUI textmesh;
    private int pop_avant;
    

    void Start()
    {
        pop_avant = VariablesEtMethodes.population;
        textmesh = GetComponent<TextMeshProUGUI>();
        textmesh.text = VariablesEtMethodes.population.ToString();      // L'argent ne peut pas dépasser 2,14 milliard
    }


    void Update() // supprimer quand on avance et remplacer par des mise a jour dans des Fixed Update
    {
        if (pop_avant != VariablesEtMethodes.population)
        {
            textmesh.text = VariablesEtMethodes.population.ToString();
            pop_avant = VariablesEtMethodes.population;
        }
    }
}
