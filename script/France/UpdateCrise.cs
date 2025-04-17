using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateCrise : MonoBehaviour
{
    // Script pour l'affichage des crises
    public Image panel;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Titre;
    public Image bouton;
    public float vitesse;
    public bool aller;
    public void retour() // Quand on appuie sur la croix
    {
        gameObject.SetActive(false);
        var Couleur = Text.color;
        var Couleur2 = Titre.color;
        var Couleur3 = panel.color;
        var Couleur4 = bouton.color;
        Couleur.a = 0;
        Couleur2.a = 0;
        Couleur3.a = 0;
        Couleur4.a = 0;
        Text.color = Couleur;
        Titre.color = Couleur2;
        panel.color = Couleur3;
        bouton.color = Couleur4;
    }
    private void activationn()// pour l'afficher
    {
        Titre.text = VariablesEtMethodes.crise_active.nom;
        Text.text = VariablesEtMethodes.crise_active.description;
        StartCoroutine(apparition());
    }
    IEnumerator apparition()
    {
        var Couleur = Text.color;
        var Couleur2 = Titre.color;
        var Couleur3 = panel.color;
        var Couleur4 = bouton.color;
        while (Titre.color.a < 1)
        {
            Couleur.a += 0.032f;
            Couleur2.a += 0.032f;
            Couleur3.a += 0.032f;
            Couleur4.a += 0.032f;
            Text.color = Couleur;
            Titre.color = Couleur2;
            panel.color = Couleur3;
            bouton.color = Couleur4;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
