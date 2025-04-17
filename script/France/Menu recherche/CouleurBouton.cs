using UnityEngine;
using UnityEngine.UI;

public class CouleurBouton : MonoBehaviour
{
    // Couleur des boutons dans la recherche
    private Button leBoutton;
    private ColorBlock laCouleur;
    void Start()
    {
        leBoutton = GetComponent<Button>();
        laCouleur = GetComponent<Button>().colors;
    }
    public void autre_appuyer()
    {
        changement_couleur(Color.green);
    }
    public void appuyer()
    {
        changement_couleur(Color.blue);
    }
    private void changement_couleur(Color couleur)
    {
        laCouleur.normalColor = couleur;
        laCouleur.highlightedColor = couleur - new Color(0.1f, 0.1f, 0.1f, 0f);
        laCouleur.pressedColor = couleur - new Color(0.1f, 0.1f, 0.1f, 0f);
        laCouleur.selectedColor = couleur - new Color(0.1f, 0.1f, 0.1f, 0f);
        leBoutton.colors = laCouleur;
    }
}
