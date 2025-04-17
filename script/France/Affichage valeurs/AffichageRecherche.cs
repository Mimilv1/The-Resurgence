using TMPro;
using UnityEngine;

public class AffichageRecherche : MonoBehaviour
{
    // Affichage des points d'education
    private TextMeshProUGUI textmesh;
    private int education_avant;


    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        textmesh.text = VariablesEtMethodes.point_education.ToString();
        education_avant = VariablesEtMethodes.point_education;
    }


    void Update() // supprimer quand on avance et remplacer par des mise a jour dans des Fixed Update
    {
        if (VariablesEtMethodes.point_education != education_avant)
        {
            textmesh.text = VariablesEtMethodes.point_education.ToString();
            education_avant = VariablesEtMethodes.point_education;
        }
    }
}
