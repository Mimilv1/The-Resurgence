using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class Cinematique : MonoBehaviour
{
    // Permet de lancer les cinematique de crise et de fin de jeu
    public VideoPlayer video_p;
    public VideoClip clip_nuke;
    public VideoClip clip_over;
    public void Lancement(string nom)
    {
        if(nom == "nuke")
        {
            video_p.clip = clip_nuke;
        }
        else if(nom == "over")
        {
            video_p.clip = clip_over;
            StartCoroutine(gestion());
        }
        video_p.Play();
    }
    IEnumerator gestion()
    {
        AudioSource source = VariablesEtMethodes.source_son.GetComponent<AudioSource>();
        source.mute = !source.mute;
        yield return new WaitForSeconds((float)video_p.clip.length);
        source.mute = !source.mute;
        gameObject.SetActive(false);
    }
}
