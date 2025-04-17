using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Video;

public class TansitionTour : MonoBehaviour
{
    // Transition quand on appuie sur le bouton du prochain tour
    private Image image;
    private TextMeshProUGUI text;

    public void transition()
    {
        gameObject.SetActive(true);
        Camera.main.GetComponent<CameraDeplacement>().peut_bouger = false;
        StartCoroutine(transi());
    }
    IEnumerator transi() // Fondu au noir et calcul
    {
        bool re_autorisation = true;
        bool stable = true; // passe a false si il y a une augmentation de 1 point de crise
        int point_c = 0;
        image = gameObject.GetComponent<Image>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Tour " + VariablesEtMethodes.tour.ToString();
        var Couleur = image.color;
        var Couleur2 = text.color;
        VariablesEtMethodes.source_son.GetComponent<JouerEffet>().effet("tour");
        while (image.color.a < 1) // Premier fondu au noir
        {
            Couleur.a += 0.022f;
            image.color = Couleur;
            yield return new WaitForSeconds(0.01f);
        }
        VariablesEtMethodes.gain_argent = (int)(VariablesEtMethodes.population * 0.1f);
        VariablesEtMethodes.point_education += VariablesEtMethodes.taux_education;
        VariablesEtMethodes.population += (int)((0.1f * VariablesEtMethodes.max_population)/(1 + Mathf.Exp((VariablesEtMethodes.population-VariablesEtMethodes.max_population)* 0.1f))
             + (int)(VariablesEtMethodes.population * 0.015f) * VariablesEtMethodes.pourcentage_augmente_pop);// fonction ecrite en s'inspirant de sigmoid 
        VariablesEtMethodes.argent += (int)(VariablesEtMethodes.gain_argent * VariablesEtMethodes.pourcentage_gain_argent);
        if (VariablesEtMethodes.demande_electrique > VariablesEtMethodes.production_electrique)
        {
            VariablesEtMethodes.temps_stabilite = 0;
            point_c += 1;
            stable = false;
        }
        if (VariablesEtMethodes.capacite_sante < VariablesEtMethodes.besoin_sante)
        {
            VariablesEtMethodes.temps_stabilite = 0;
            point_c += 1;
            stable = false;
        }
        if (VariablesEtMethodes.argent < 0)
        {
            VariablesEtMethodes.temps_stabilite = 0;
            point_c += 1;
            stable = false;
        }
        if (stable)
        {
            VariablesEtMethodes.temps_stabilite += 1;
            for (int i = VariablesEtMethodes.point_crise.Count() - 1; i >= 0; i--)
            {
                if (VariablesEtMethodes.point_crise[i])
                {
                    VariablesEtMethodes.point_crise[i] = false;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < VariablesEtMethodes.point_crise.Count(); i++)
            {
                if (!VariablesEtMethodes.point_crise[i])
                {
                    VariablesEtMethodes.point_crise[i] = true;
                    point_c -= 1;
                }
                if (point_c == 0)
                {
                    break;
                }
            }
        }
        // test perdu
        int compteur = 0;
        bool perdu = false;
        foreach (bool valeur in VariablesEtMethodes.point_crise)
        {
            if (valeur)
            {
                compteur += 1;
            }
            else
            {
                break;
            }
        }
        if (compteur == VariablesEtMethodes.point_crise.Count())
        {
            VariablesEtMethodes.relai.SendMessage("End");
            perdu = true;
        }
        // suite pour le tour
        if (!perdu)
        {
            if (VariablesEtMethodes.tour_crise == VariablesEtMethodes.tour)
            {
                if (VariablesEtMethodes.les_crises.Count > 0)
                {
                    VariablesEtMethodes.les_crises[Random.Range(0, VariablesEtMethodes.les_crises.Count)].activation();
                }
                VariablesEtMethodes.tour_prochaine_crise();
            }

            foreach (Crise crise in VariablesEtMethodes.les_crises_active.ToList())
            {
                crise.decompte();
            }
            if (VariablesEtMethodes.elec_acheter == 0 && VariablesEtMethodes.cour_elec > 80)
            {
                VariablesEtMethodes.cour_elec -= Random.Range(10, 30);
            }
            if (VariablesEtMethodes.sante_acheter == 0 && VariablesEtMethodes.cour_sante > 10)
            {
                VariablesEtMethodes.cour_sante -= Random.Range(1, 3);
            }
            if (VariablesEtMethodes.pop_acheter == 0 && VariablesEtMethodes.immigration_cour > 5)
            {
                VariablesEtMethodes.immigration_cour -= 1;
            }
            VariablesEtMethodes.elec_acheter = 0;
            VariablesEtMethodes.sante_acheter = 0;
            VariablesEtMethodes.pop_acheter = 0;
            VariablesEtMethodes.calcul_bat();

            yield return new WaitForSecondsRealtime(0.3f);
            while (text.color.a < 1) // Fondu du text
            {
                Couleur2.a += 0.032f;
                text.color = Couleur2;
                yield return new WaitForSeconds(0.01f);
            }
            if ((VariablesEtMethodes.temps_stabilite >= 150 || VariablesEtMethodes.tour == 500) && !VariablesEtMethodes.gagner) // Test de victoire
            {
                VariablesEtMethodes.gagner = true;
                VariablesEtMethodes.relai.SendMessage("Win");
            }
            yield return new WaitForSeconds(0.5f);
            while (image.color.a > 0) // disparition en transparent des deux
            {
                Couleur.a -= 0.032f;
                Couleur2.a -= 0.032f;
                image.color = Couleur;
                text.color = Couleur2;
                yield return new WaitForSeconds(0.01f);
            }
            VariablesEtMethodes.relai.activation();
            Camera.main.GetComponent<CameraDeplacement>().peut_bouger = true;
            gameObject.SetActive(false);
            VariablesEtMethodes.relai.BroadcastMessage("apuyable");
            if (re_autorisation)
            {
                VariablesEtMethodes.bouton_exit.GetComponent<Pause>().autorisation = true;
            }
        }
    }
}
