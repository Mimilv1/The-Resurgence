using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    // Script qui s'occupe des constructions de batiments
    public GameObject affichage;
    public GameObject ville;
    public GameObject eolienne;
    public GameObject charbon;
    public GameObject clinique;
    public GameObject ecole;
    public GameObject lycee;
    public GameObject universite;
    public GameObject nuke;
    public GameObject maison_sante;
    public GameObject hopital;
    public GameObject sous_emplacement;
    public GameObject sous_emplacement_2;
    public GameObject sous_emplacement_3;
    public Transform parent;
    public RelaiMessage relai;
    public Button b_nucleaire;
    public Button b_charbon;
    public Button b_lycee;
    public Button b_universite;
    public Button b_maison_sante;
    public Button b_hopital;
    public Button b_ville_2;
    public Button b_ville_3;
    private void Start()
    {
        test_recherche();
    }
    void OnEnable()// se lance des que le game Object devient actif
    {
        test_recherche();
    }
    private void test_recherche()
    {
        if (b_nucleaire != null) // test que le message est pas reçu par un emplacement de construction de ville
        {
            if (VariablesEtMethodes.recherche[0])
            {
                b_nucleaire.interactable = true;
            }
            if (VariablesEtMethodes.recherche[1])
            {
                b_charbon.interactable = true;
            }
            if (VariablesEtMethodes.recherche[2])
            {
                b_lycee.interactable = true;
            }
            if (VariablesEtMethodes.recherche[3])
            {
                b_universite.interactable = true;
            }
            if (VariablesEtMethodes.recherche[4])
            {
                b_maison_sante.interactable = true;
            }
            if (VariablesEtMethodes.recherche[5])
            {
                b_hopital.interactable = true;
            }
        }
        else
        {
            if (VariablesEtMethodes.recherche[6])
            {
                b_ville_2.interactable = true;
            }
            if (VariablesEtMethodes.recherche[7])
            {
                b_ville_3.interactable = true;
            }
        }
    }
    private void Destruction() // detruit totalement un sous batiments (est appeler par message
    {
        Destroy(gameObject);
    }

    public void change()
    {
        bool etat = !affichage.activeSelf;
        relai.efface();
        affichage.SetActive(etat);
    }

    public void desactive()
    {
        affichage.SetActive(false);
    }

    public void Construire_ville(int amelio) // Fonction qui est appele lors de l'apui du bouton ville
    {
        if (VariablesEtMethodes.argent >= VariablesEtMethodes.prix_ville)
        {
            VariablesEtMethodes.source_son.GetComponent<JouerEffet>().effet("argent");
            Destroy(gameObject);
            VariablesEtMethodes.argent -= VariablesEtMethodes.prix_ville;
            VariablesEtMethodes.les_batiment.Add(1);
            VariablesEtMethodes.calcul_bat();
            GameObject clone_v = Instantiate(ville, transform.position, Quaternion.identity, parent);
            clone_v.SetActive(true);
            ScriptBatiment script = clone_v.GetComponentInChildren<ScriptBatiment>();
            script.parent = parent;
            //clone.GetComponent<ScriptBatiment>().parent = parent ;
            GameObject clone = Instantiate(sous_emplacement, transform.position, Quaternion.identity, parent); // Initialisation des sous emplacements
            GameObject clone2 = Instantiate(sous_emplacement_2, transform.position, Quaternion.identity, parent);
            GameObject clone3 = Instantiate(sous_emplacement_3, transform.position, Quaternion.identity, parent);
            script.sous_batiment_1 = clone;
            script.sous_batiment_2 = clone2;
            script.sous_batiment_3 = clone3;
            clone.SetActive(true);
            script.sous_batiment.Add(clone);
            if (amelio >= 1)
            {
                script.Upgrade_ville();
                if (amelio == 2)
                {
                    script.Upgrade_ville();
                }
            }
        }
    }

    public void Construire_eolienne()
    {
        Construire(VariablesEtMethodes.prix_eolienne, 101, eolienne);
    }

    public void Construire_charbon()
    {
        Construire(VariablesEtMethodes.prix_usine_charbon ,111, charbon);
    }
    public void Construire_nucleaire()
    {
        Construire(VariablesEtMethodes.prix_nuke, 121, nuke);
    }

    public void Construire_clinique()
    {
        Construire(VariablesEtMethodes.prix_clinique, 1001, clinique);
    }
    public void Construire_maison_sante()
    {
        Construire(VariablesEtMethodes.prix_maison_sante, 1011, maison_sante);
    }
    public void Construire_hopital()
    {
        Construire(VariablesEtMethodes.prix_hopital, 1021, hopital);
    }

    public void Construire_ecole()
    {
        Construire(VariablesEtMethodes.prix_ecole, 10001, ecole);
    }
    public void Construire_lycee()
    {
        Construire(VariablesEtMethodes.prix_lycee, 10011, lycee);
    }
    public void Construire_universtite()
    {
        Construire(VariablesEtMethodes.prix_lycee, 10021, universite);
    }

    public void Construire(int prix, int id, GameObject objet)
    {
        if (VariablesEtMethodes.argent >= prix)
        {
            VariablesEtMethodes.source_son.GetComponent<JouerEffet>().effet("argent");
            Destroy(gameObject);
            VariablesEtMethodes.argent -= prix;
            VariablesEtMethodes.les_batiment.Add(id);
            VariablesEtMethodes.calcul_bat();
            GameObject clone = Instantiate(objet, transform.position, Quaternion.identity, parent);
            clone.SetActive(true);
            clone.GetComponentInChildren<ScriptBatiment>().parent = parent;
            //clone.GetComponent<ScriptBatiment>().parent = parent ;
        }
    }
}