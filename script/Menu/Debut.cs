using UnityEngine;

public class Debut : MonoBehaviour // MonoBehaviour veut dire script dans unity en gros
{


    void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        // Permet de limiter les FPS au Hz de l'écran de l'utilisateur
        // Ne pas toucher aux scènes dans une fonction Awake
    }
}
