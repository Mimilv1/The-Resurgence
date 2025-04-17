using UnityEngine;
using System.Collections.Generic;

public class ScriptBatiment : MonoBehaviour 
{
    public GameObject affichage;
    public GameObject construction;
    public GameObject bouton_upgrade;
    public Transform parent;
    public RelaiMessage relai;
    public GameObject superieur;
    public List<GameObject> sous_batiment; // pour les villes
    public GameObject sous_batiment_1;
    public GameObject sous_batiment_2;
    public GameObject sous_batiment_3;
    public int id;
    public int prix_upgrade; // prix de la première amélioration puis des suivantes
    public int[] prix_up; // prix de la deuxième et troisième upgrade
    private int id_base = 0;
    private int max_up = 1;
    private int niveau = 0;
    private void Start()
    {
        id_base = id;
        if (max_up == 1)
        {
            max_up += prix_up.Length;
        }
        affichage.transform.position = parent.transform.position + new Vector3(0,20,0);
    }


    public void change()
    {
        bool etat = !affichage.activeSelf;
        relai.efface();
        affichage.SetActive(etat); // On met le gameobject affichage dans l'etat inverse de son état actuelle
    }


    public void desactive()
    {
        affichage.SetActive(false);
    }
    public void Detruire()
    {
        Destruction();
        GameObject clone = Instantiate(construction, transform.position, Quaternion.identity, parent);
        clone.SetActive(true);
        clone.GetComponent<BuildingButton>().parent = parent;
    }

    public void Destruction() // detruit totalement un sous batiments
    {
        if(id>120 && id < 125)
        {
            VariablesEtMethodes.Nuke.Remove(gameObject);
        }
        Destroy(superieur);
        VariablesEtMethodes.les_batiment.Remove(id);
        VariablesEtMethodes.calcul_bat();
    }
    public void Destruction_ville()
    {
        foreach(GameObject objet in sous_batiment) // pour detruire les batiments
        {
            objet.BroadcastMessage("Destruction");
        }
        foreach(GameObject objet in sous_batiment) // pour detruire les objet de sous emplacement (les parents des autre batiment)
        {
            Destroy(objet);
        }
        GameObject construi = Instantiate(construction, transform.position, Quaternion.identity, parent);
        construi.SetActive(true);
        construi.GetComponent<BuildingButton>().parent = parent;
        Destruction();
    }

    public void Upgrade()
    {
        if (max_up == 1)
        {
            max_up += prix_up.Length;
        }
        if (VariablesEtMethodes.argent >= prix_upgrade)
        {
            VariablesEtMethodes.source_son.GetComponent<JouerEffet>().effet("argent");
            VariablesEtMethodes.argent -= prix_upgrade;
            VariablesEtMethodes.les_batiment.Remove(id);
            VariablesEtMethodes.les_batiment.Add(id + 1);
            id += 1;
            niveau++;
            if (niveau < max_up)// On test les valeurs de L'id pour savoir quel sera le prix de l'amelioration on utilise pas switch car les valeurs sont variables
            {
                prix_upgrade = prix_up[niveau - 1];
                VariablesEtMethodes.calcul_bat();
            }
            else 
            {
                Destroy(bouton_upgrade);
                VariablesEtMethodes.calcul_bat();
            }
        }
    }

    public void Upgrade_ville()
    {
        Upgrade();
        GameObject emplacement; // on l'assigne pour pas qu'il n'est pas de valeur
        if (niveau == 1)
        {
            emplacement = sous_batiment_2;
            emplacement.SetActive(true);
            sous_batiment.Add(emplacement);
        }
        else if (niveau == 2)
        {
            emplacement = sous_batiment_3;
            emplacement.SetActive(true);
            sous_batiment.Add(emplacement);
        }
    }
}
