using UnityEngine;
using UnityEngine.Video;

public class GestionVideoIntro : MonoBehaviour
{
    //Lance la video quand on lance le jeu
    public VideoPlayer video;
    public GameObject jeu;
    private bool a_ete_lance;
    void Update()
    {
        if (video.isPlaying)
        {
            a_ete_lance = true;
        }
        else if (!video.isPlaying && a_ete_lance)
        {
            jeu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            video.Stop();
            jeu.SetActive(true);
        }
    }
}
