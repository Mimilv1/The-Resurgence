using UnityEngine;

public class Playlist : MonoBehaviour
{
    // Script pour la playlist de la musique
    public AudioClip[] playlist;
    public AudioSource le_master;
    private int index;

    private void Start()
    {
        index = 7;
        le_master.clip = playlist[index];
        le_master.Play();
    }

    private void Update()
    {
        if (!le_master.isPlaying)
        {
            prochain_son();
        }
    }
    private void prochain_son()
    {
        index = (index + 1) % playlist.Length; // Congruence
        le_master.clip = playlist[index];
        le_master.Play();
    }
}
