using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Recherche : MonoBehaviour
{
    // Syteme qui lie les differrentes recherches entre elle
    public List<Recherche> Les_prochaines;
    public List<Slider> Les_slider;
    public bool aquis;
    public int prix;
    public string description;
    private bool en_cour = false;

    public void Activation(string identifiant) // limiter a 1 un argument pour les fonctions appeler par les boutons
    {
        if (VariablesEtMethodes.point_education >= prix)
        {
            en_cour = true;
            VariablesEtMethodes.source_son.GetComponent<JouerEffet>().effet("recherche");
            VariablesEtMethodes.point_education -= prix;
            gameObject.GetComponent<Button>().interactable = false;
            aquis = true;
            if (identifiant == "1_pop")
            {
                ToutesLesRecherches.recherche_pop_gain(20);
            }
            else if (identifiant == "2_pop")
            {
                ToutesLesRecherches.recherche_pop_gain(25);
                ToutesLesRecherches.recherche_batiment(6);
            }
            else if (identifiant == "3_pop")
            {
                ToutesLesRecherches.recherche_pop_max(25);
            }
            else if (identifiant == "5_pop")
            {
                ToutesLesRecherches.recherche_pop_max(20);
            }
            else if (identifiant == "6_pop")
            {
                ToutesLesRecherches.recherche_pop_max(10);
                ToutesLesRecherches.recherche_pop_gain(10);
                ToutesLesRecherches.recherche_batiment(7);
            }
            else if (identifiant == "1_elec")
            {
                ToutesLesRecherches.recherche_batiment(0);
            }
            else if (identifiant == "2_elec")
            {
                ToutesLesRecherches.recherche_changement_nuke(50);
            }
            else if (identifiant == "3_elec")
            {
                ToutesLesRecherches.recherche_changement_nuke(25);
            }
            else if (identifiant == "4_elec")
            {
                ToutesLesRecherches.recherche_changement_nuke(1);
            }
            else if (identifiant == "5_elec")
            {
                ToutesLesRecherches.recherche_batiment(1);
            }
            else if (identifiant == "6_elec")
            {
                ToutesLesRecherches.recherche_prix_charbon(0.5f);
            }
            else if (identifiant == "7_elec")
            {
                ToutesLesRecherches.recherche_prix_charbon(0.25f);
            }
            else if (identifiant == "8_elec")
            {
                ToutesLesRecherches.recherche_eolienne_prod(2);
            }
            else if (identifiant == "9_elec")
            {
                ToutesLesRecherches.recherche_eolienne_prod(4);
            }
            else if (identifiant == "1_san")
            {
                ToutesLesRecherches.recherche_batiment(4);
            }
            else if (identifiant == "2_san")
            {
                ToutesLesRecherches.recherche_batiment(5);
            }
            else if (identifiant == "3_san")
            {
                ToutesLesRecherches.recherche_capacite_sante(10);
                ToutesLesRecherches.recherche_demande_sante(10);
            }
            else if (identifiant == "4_san")
            {
                ToutesLesRecherches.recherche_capacite_sante(5);
                ToutesLesRecherches.recherche_demande_sante(5);
            }
            else if (identifiant == "5_san")
            {
                ToutesLesRecherches.recherche_reduction_sante(0.1f);
            }
            else if (identifiant == "6_san")
            {
                ToutesLesRecherches.recherche_reduction_sante(0.15f);
            }
            else if (identifiant == "1_edu")
            {
                ToutesLesRecherches.recherche_pourcentage_education(0.2f);
            }
            else if (identifiant == "2_edu")
            {
                ToutesLesRecherches.recherche_prix_education(0.1f);
            }
            else if (identifiant == "3_edu")
            {
                ToutesLesRecherches.recherche_batiment(2);
            }
            else if (identifiant == "4_edu")
            {
                ToutesLesRecherches.recherche_batiment(3);
            }
            else if (identifiant == "5_edu")
            {
                ToutesLesRecherches.recherche_pourcentage_education(1);
            }
            else
            {
                Debug.Log("Recherche inconnu");
            }
            foreach (Slider slider in Les_slider)
            {
                StartCoroutine(animationn(slider));
            }
        }
    }


    IEnumerator animationn(Slider slide)
    {
        for (int i = 0; i < 101; i++) // 101 pour ne pas que la valeur de value soit 0.99999 car c'est des addition de float
        {
            slide.value += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Recherche prochain in Les_prochaines)
        {
            prochain.accsessible();
        }
        en_cour = false;
    }


    public void accsessible()
    {
        if (!gameObject.GetComponent<Recherche>().aquis)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }


    private void OnDisable()// fixe le bug du slider qui ne termine pas son animation et bloque les prochaines recherche quand on changeait d'interface
    {
        if (en_cour)
        {
            foreach (Slider slider in Les_slider)
            {
                slider.value = 1;
            }
            foreach (Recherche prochain in Les_prochaines)
            {
                prochain.accsessible();
            }
        }
    }
}
