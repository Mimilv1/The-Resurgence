using UnityEngine;
using UnityEngine.UI;

public class PressedButton : MonoBehaviour
{
    private Button leBoutton;
    private ColorBlock laCouleur;
    private ColorBlock CouleurBase;


    void Awake()
    {
        leBoutton = GetComponent<Button>();
        laCouleur = GetComponent<Button>().colors;
        CouleurBase = laCouleur;
        switch (VariablesEtMethodes.difficulte) // on va verifie la valeur de difficulte et changer la couleur du bon bouton paraport à la valeur 
            // de difficulte remarque ce script est présent dans chaque bouton de choix de difficulte c'est pour ca que l'on verifie son nom
            // pour ne pas avoir un script pour chaque bouton
        {
            case 1: // switch prend en compte une valeur utiliser dans les cases qui veut dire cas si on le traduit 
                // ici on a donc le cas ou difficulte vaut 1 cette syntax existe en python 3.10 avec match
                if(gameObject.name == "Bouton facile")
                {
                    press();
                }
                break;
            case 2:
                if (gameObject.name == "Bouton moyen")
                {
                    press();
                }
                break;
            case 3:
                if (gameObject.name == "Bouton difficile")
                {
                    press();
                }
                break;
            default:
                if (gameObject.name == "Bouton facile")
                {
                    press();
                }
                break;
        }
    }


    public void press() // appeler quand le boouton est pressé et permet de le laisser en Rouge
    {
        laCouleur.normalColor = Color.gray;
        laCouleur.highlightedColor = Color.gray;
        laCouleur.pressedColor = Color.gray;
        laCouleur.disabledColor = Color.gray;
        leBoutton.colors = laCouleur;
    }


    public void mise_a_jour() // appeler quand un autre bouton est appuyer
    {
        leBoutton.colors = CouleurBase;
    }
}
