using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Deplacement : MonoBehaviour
{
    public float vitesse = 3f;
    public List<Button> les_boutonsG1;
    public List<Button> les_boutonsG2;
    private bool en_deplacement;
    private void Start()
    {
        en_deplacement = false;
    }
    private void bouton_appuiable(bool activer, List<Button> la_liste)// permet de désactiver ou d'activer tous les boutons d'une liste
    {
        foreach (Button bouton in la_liste)
        {
            bouton.interactable = activer;
        }
    }


    public void A_gauche() // Fonction appeler quand on appuie sur le premier bouton jouer
    {
        if (!en_deplacement)
        {
            en_deplacement = true;
            bouton_appuiable(false, les_boutonsG1);
            bouton_appuiable(true, les_boutonsG2);
            StartCoroutine(gauche());
        }
    }


    public void A_droite() // Fonction appeler quand on appuie sur le deuxieme bouton jouer
    {
        if (!en_deplacement)
        {
            en_deplacement = true;
            bouton_appuiable(false, les_boutonsG2);
            bouton_appuiable(true, les_boutonsG1);
            StartCoroutine(droite());
        }
    }


    IEnumerator droite()
    {
        while (transform.position.x < Screen.width * 0.21f)
        {
            transform.position += new Vector3(Screen.width * vitesse * Time.deltaTime, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        en_deplacement = false;
    }


    IEnumerator gauche()
    {
        while (transform.position.x > -Screen.width * 0.85f)
        {
            transform.position += new Vector3(-Screen.width * vitesse * Time.deltaTime, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        en_deplacement = false;
    }
}