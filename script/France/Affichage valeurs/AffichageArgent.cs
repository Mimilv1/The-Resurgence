using UnityEngine;
using TMPro;

public class AffichageArgent : MonoBehaviour
{
    // permet l'affichage de l'argent
    private TextMeshProUGUI textmesh;
    private int argent_avant;


    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        textmesh.text = VariablesEtMethodes.argent.ToString() + " K";
        argent_avant = VariablesEtMethodes.argent;          // L'argent ne peut pas dépasser 2,14 milliard
    }


    void Update()
    {
        if (VariablesEtMethodes.argent != argent_avant)
        {
            textmesh.text = VariablesEtMethodes.argent.ToString() + " K";
            argent_avant = VariablesEtMethodes.argent;
        }
    }
}
