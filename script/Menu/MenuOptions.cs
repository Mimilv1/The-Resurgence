using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MenuOptions : MonoBehaviour
{
    public Dropdown dropdown;
    public AudioMixer audioMixer;
    public Toggle toggle;
    public Slider slider;
    public Slider slider_music;
    public Slider slider_effet;
    Resolution[] resolution; // utilisation d'un array


    void Awake()
    {
        if (VariablesEtMethodes.valeur_volume_Global != 0f)
        {
            slider.value = VariablesEtMethodes.valeur_volume_Global;
        }
        else
        {
            slider.value = 1;
        }
        if (VariablesEtMethodes.valeur_volume_music != 0f)
        {
            slider_music.value = VariablesEtMethodes.valeur_volume_music;
        }
        else
        {
            slider_music.value = 1;
        }
        if (VariablesEtMethodes.valeur_volume_effet != 0f)
        {
            slider_effet.value = VariablesEtMethodes.valeur_volume_effet;
        }
        else
        {
            slider_effet.value = 1;
        }
        resolution = Screen.resolutions.Select(resolutionss => new Resolution { width = resolutionss.width, height = resolutionss.height }).Distinct().ToArray();
        // prend toutes les valeurs des résolutions disponible sur l'écran avec le select et on utilise distinct pour ne pas avoir de doublon
        dropdown.ClearOptions();
        int ResolutionActuelle = 0;
        List<string> choix = new List<string>();
        for (int i = 0; i < resolution.Length; i++)
        {
                choix.Add(resolution[i].width + "x" + resolution[i].height);
                if (resolution[i].width == Screen.width && resolution[i].height == Screen.height)
                {
                    ResolutionActuelle = i;
                }
        }
        dropdown.AddOptions(choix);
        dropdown.value = ResolutionActuelle; // La resolution actuelle est déja set donc pas besoins de la set vu que c'est la résolution de départ
        dropdown.RefreshShownValue(); // on met juste le choix des resolutions au bonne endroit
        toggle.isOn = Screen.fullScreen;
        gameObject.SetActive(false);
    }


    public void SetResolution(int indice)
    {
        Resolution la_resolution = resolution[indice];
        Screen.SetResolution(la_resolution.width, la_resolution.height, Screen.fullScreen);
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("VolumeGlobal", Mathf.Log10(volume) * 20);
        VariablesEtMethodes.valeur_volume_Global = volume;
    }
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        VariablesEtMethodes.valeur_volume_music = volume;
    }
    public void SetEffet(float volume)
    {
        audioMixer.SetFloat("Effet", Mathf.Log10(volume) * 20);
        VariablesEtMethodes.valeur_volume_effet = volume;
    }


    public void Pleine_Ecran(bool etat)
    {
        Screen.fullScreen = etat;
    }
}
