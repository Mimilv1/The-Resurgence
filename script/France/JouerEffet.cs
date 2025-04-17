using UnityEngine;

public class JouerEffet : MonoBehaviour
{
    public AudioSource son;
    public AudioClip achat;
    public AudioClip recherche;
    public AudioClip sourd;
    public AudioClip deplacement;
    public AudioClip prochain_tour;

    public void effet(string nom)
    {
        if (nom == "argent")
        {
            son.clip = achat;
        }
        else if (nom == "recherche")
        {
            son.clip = recherche;
        }
        else if(nom == "sourd")
        {
            son.clip = sourd;
        }
        else if (nom == "deplacement")
        {
            son.clip = deplacement;
        }
        else if (nom == "tour")
        {
            son.clip = prochain_tour;
        }
        son.Play();
    }
}
