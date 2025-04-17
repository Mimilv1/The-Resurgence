using UnityEngine;
using TMPro;

public class AffichageDescription : MonoBehaviour
{
    // text present dans les gameobjct de description de recherche
    private TextMeshProUGUI textmesh;
    public Recherche recherche;
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        textmesh.text = recherche.description.ToString();
    }
}
