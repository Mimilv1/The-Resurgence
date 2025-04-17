using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DescriInterractive : MonoBehaviour
{
    // Affichage des descriptions des rechercehes quand on passe la souris au dessu
    public GameObject le_text;
    public Image fond;
    public TextMeshProUGUI text;
    private bool entree_encour = false;
    private bool sortie_encour = false;
    private void Start()
    {
        Physics.queriesHitTriggers = true;
    }
    private void OnMouseEnter()
    {
        StartCoroutine(transition(true));
    }
    private void OnMouseExit()
    {
        StartCoroutine(transition(false));
    }
    IEnumerator transition(bool mode)
    {
        if (mode)
        {
            while (sortie_encour) // ici on attemps que l'animation se finisse de l'autre côté
            {
                yield return new WaitForSeconds(0.01f);
            }
            le_text.SetActive(true);
            entree_encour = true;
            for (int i = 0; i < 25; i++)
            {
                var Couleur = fond.color;
                var Couleur2 = text.color;
                Couleur.a += 0.032f;
                Couleur2.a += 0.032f;
                text.color = Couleur2;
                fond.color = Couleur;
                yield return new WaitForSeconds(0.01f);
            }
            entree_encour = false;
        }
        else
        {
            while (entree_encour) // ici on attemps que l'animation se finisse de l'autre côté
            {
                yield return new WaitForSeconds(0.01f);
            }
            sortie_encour = true;
            for (int i = 0; i < 25; i++)
            {
                var Couleur = fond.color;
                var Couleur2 = text.color;
                Couleur.a -= 0.032f;
                Couleur2.a -= 0.032f;
                text.color = Couleur2;
                fond.color = Couleur;
                yield return new WaitForSeconds(0.01f);
            }
            sortie_encour = false;
            le_text.SetActive(false);
        }
    }
    void OnDisable() // se déclanche quand le gameObject devient inactif
    {
        sortie_encour = false;
        entree_encour = false;
        var Couleur = fond.color;
        var Couleur2 = text.color;
        Couleur.a = 0;
        Couleur2.a = 0;
        text.color = Couleur2;
        fond.color = Couleur;
        le_text.SetActive(false);
    }
}
