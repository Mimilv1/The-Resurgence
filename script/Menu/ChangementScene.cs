using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class ChangementScene : MonoBehaviour
{
    // static fait qu'il n'existe qu'une seule valeur de difficulte dans tous les objets VariablesEtMethodes
    // ainsi n'importe quel script peut y accéder car il n'y en a que une en faisant VariablesEtMethodes.difficulte
    // remarque des class entière peuvent êtres statiques
    public Slider slider;
    public TextMeshProUGUI Progression_text;
    public string nom_scene;

    public void modifier(string texte)
    {
        nom_scene = texte;
    }
    public void PlayButon(string texte)
    {
        if (texte == "")
        {
            StartCoroutine(LoadAsync(nom_scene));
        }
        else
        {
            StartCoroutine(LoadAsync(texte));
        }
        // Fonction appeler lorsque que l'on appuie sur Jouer
    }


    IEnumerator LoadAsync(string scene)// Coroutine qui s'execute en même temps que le reste du script mais en parrallele
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);// va permettre de charger la scene avant de la lancer
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            Progression_text.text = Math.Round((float)(progress * 100)) + "%";
            yield return null;
        }
    }


    public void Choix_difficulte(int difficulte)
    {
        VariablesEtMethodes.difficulte = difficulte;
    }
}
