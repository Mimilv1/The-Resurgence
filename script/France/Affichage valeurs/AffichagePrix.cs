using UnityEngine;
using TMPro;

public class AffichagePrix : MonoBehaviour
{
    // Affich le pric des recherches
    private TextMeshProUGUI textmesh;
    public Recherche Objet;
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        textmesh.text = Objet.prix.ToString();
    }
}