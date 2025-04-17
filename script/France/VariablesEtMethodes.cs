using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
// Sigelton principale du jeu
public class VariablesEtMethodes : MonoBehaviour
{
    // Initialisation
    public static int tour = 0;
    public static bool lance = false; // Permet de savoir si c'est la premiere partie lancer
    public static bool gagner = false;
    // Ressources
    public static int argent = 1600;
    public static int gain_argent = 0;
    public static int max_population = 100;
    public static int population = 10;
    public static int capacite_sante = 0;
    public static int besoin_sante = 0;
    public static int taux_education = 0;
    public static int point_education = 0;
    public static int demande_electrique = 0;
    public static int production_electrique = 0;
    // Facteur des differentes valeurs
    public static float pourcentage_taux_education = 1.0f;
    public static float pourcentage_production_electrique = 1.0f;
    public static float pourcentage_max_population = 1.0f;
    public static float pourcentage_capacite_sante = 1.0f;
    public static float pourcentage_demande_electrique = 1.0f;
    public static float pourcentage_gain_argent = 1.0f;
    public static float pourcentage_besoin_sante = 1.0f;
    public static float pourcentage_augmente_pop = 1.0f;
    public static float pourcentage_production_eolienne = 1.0f;
    public static float pourcentage_prix_du_charbon = 1.0f;
    // Gestion des batiments
    public static int difficulte = 1;
    public static List<int> les_batiment = new List<int>();
    public static List<GameObject> Nuke = new List<GameObject>();
    public static int periode = 0;
    // Les prix
    public static int prix_ville = 300;
    public static int prix_eolienne = 300;
    public static int prix_clinique = 300;
    public static int prix_ecole = 500;
    public static int prix_usine_charbon = 500;
    public static int prix_nuke = 2500;
    public static float prix_du_charbon = 50;
    public static int prix_lycee = 700;
    public static int prix_universite = 2000;
    public static int prix_maison_sante = 500;
    public static int prix_hopital = 2000;
    // Gestion recherche
    public static bool[] recherche = {false, false, false, false, false, false, false, false}; // centrale nucleaire, centrale charbon, lycee, universite, maison de sante , ville n2, ville n3
    // Partie sur les crise
    public static int temps_stabilite = 0;
    private static int range;
    public static bool du_vent = true;
    public static int chance_nuke = 0;
    public static bool[] point_crise = {false, false, false, false, false};
    public static int point_crise_max = 5;// Si on arrive a 5 point de crise on perd
    public static int tour_crise = 0; // numéro du tour durant lequelle il y aura la prochaine crise
    public static List<Crise> les_crises = new List<Crise>();
    public static Crise crise_active;
    public static List<Crise> les_crises_active = new List<Crise>();
    public static int duree_crise = 4; // la durée des crises en tour -1 Donc 4 = 5 tour avant que sa revient
    // Gameobject dont j'ai besoin
    public static GameObject pop_up; // Le pop up doit être static et est défini dans le start
    public static RelaiMessage relai; // pour envoyer des messages à presque tous les GameObject
    public static GameObject fond_tour_apres;
    public static GameObject bouton_exit; // pour gerer l'autorisation d'utiliser le bouton menu
    public static GameObject video_player;
    public static GameObject source_son;
    // Partie affichage des stats
    public static TextMeshProUGUI affichage_pourcent_gain_argent;
    public static TextMeshProUGUI affichage_pourcent_elec;
    public static TextMeshProUGUI affichage_pourcent_sante;
    public static TextMeshProUGUI affichage_pourcent_education;
    public static TextMeshProUGUI affichage_pourcent_gain_pop;
    // Cour du marche internationale
    public static int cour_elec = 100;
    public static int cour_sante = 10;
    public static int pop_acheter = 0;
    public static int elec_acheter = 0;
    public static int sante_acheter = 0;
    public static int immigration_cour = 5;
    // Son
    public static float valeur_volume_Global = 0f;
    public static float valeur_volume_music = 0f;
    public static float valeur_volume_effet = 0f;

    public static void Nouvelle_crise(string nom, string descri, string cible, int pourcent, string type = null)
    {
        les_crises.Add(new Crise(nom, descri, cible, pourcent, type));
    }


    void Awake()
    {
        if (!lance)
        {
            initialisation();
            lance = true;
        }
        else
        {
            nouvelle_game();
            initialisation();
        }
    }
    public static void initialisation()
    {
        pop_up = GameObject.Find("Pop UP crise");
        relai = GameObject.Find("Jeu").GetComponent<RelaiMessage>();
        bouton_exit = GameObject.Find("Exit");
        source_son = GameObject.Find("Son");
        // affichage des stats gérer dans Variables et methodes pour limiter le nombre de script
        affichage_pourcent_gain_argent = (GameObject.Find("Argent (TMP)")).GetComponent<TextMeshProUGUI>();
        affichage_pourcent_elec = (GameObject.Find("Elec (TMP)")).GetComponent<TextMeshProUGUI>();
        affichage_pourcent_sante = (GameObject.Find("Sante (TMP)")).GetComponent<TextMeshProUGUI>();
        affichage_pourcent_education = (GameObject.Find("Recherche (TMP)")).GetComponent<TextMeshProUGUI>();
        affichage_pourcent_gain_pop = (GameObject.Find("Pop (TMP)")).GetComponent<TextMeshProUGUI>();
        Affichage_update();
        fond_tour_apres = GameObject.Find("Fond Prochain Tour");
        fond_tour_apres.SetActive(false);
        pop_up.SetActive(false);
        besoin_sante = population / 5;
        switch (difficulte) // j'aurais pus le faire avec des if mais la syntaxe est 10 fois plus simple
        {
            case 1:
                periode = 9;
                range = 31;
                break;
            case 2:
                periode = 5;
                range = 41;
                break;
            case 3:
                periode = 2;
                range = 51;
                break;
            default:
                periode = 15;
                range = 31;
                break;
        }
        
        int pourcentage = Random.Range(20, range);
        
        Nouvelle_crise("Crack Boursier", "Perte de " + pourcentage.ToString() + "% de l'argent actuelle", "argent", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Exode massive", "Perte de " + pourcentage.ToString() + "% de la population actuelle", "population", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Fraude massive", "Perte de " + pourcentage.ToString() + "% des gain d'argent actuelle", "gain argent", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Infrastructures fragiles", "Perte de " + pourcentage.ToString() + "% du maximum de la population", "max population", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Periode froide", "Augmentation de " + pourcentage.ToString() + "% de la demande electrique", "demande elec", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Greve au departement de l'energie", "Perte de " + pourcentage.ToString() + "% de la production electrique", "production elec", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Greve au departement de la sante", "Perte de " + pourcentage.ToString() + "% de la capacite en sante", "capacite sante", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Epidemie", "Augmentation de " + pourcentage.ToString() + "% du besoin de santé", "besoin sante", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Vacance prolonge", "Perte de " + pourcentage.ToString() + "% du taux d'education", "taux education", pourcentage);
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Augmentation des prix", "Augmentation de " + pourcentage.ToString() + "% des cours de l'electricite et de la sante et de la naturalisation", "cour", pourcentage);
        
        pourcentage = Random.Range(20, range);
        Nouvelle_crise("Manque de materiaux", "Augmentation des prix de tout les batiments de " + pourcentage.ToString(), "prix", pourcentage);
        /*
        les_crises[0].activation(); // 0 B argent actuelle
        les_crises[1].activation(); // 1 B population actuelle
        les_crises[2].activation(); // 2 B gain argent
        les_crises[3].activation(); // 3 B max population
        les_crises[4].activation(); // 4 B demande elec
        les_crises[5].activation(); // 5 B production elec
        les_crises[6].activation(); // 6 B capacite sante
        les_crises[7].activation(); // 7 B besoin sante
        les_crises[8].activation(); // 8 B taux education
        les_crises[9].activation(); // 9 B cour
        les_crises[10].activation(); // 10 B prix des batiments
        */
        tour_prochaine_crise();
    }


    public static void nouvelle_game() // remet les valeurs de base des differentes variables de jeu
    {
        gagner = false;
        tour = 1;
        argent = 1600;
        gain_argent = 0;
        max_population = 100;
        population = 10;
        capacite_sante = 0;
        besoin_sante = 0;
        taux_education = 0;
        point_education = 0;
        demande_electrique = 0;
        production_electrique = 0;
        pourcentage_taux_education = 1.0f;
        pourcentage_production_electrique = 1.0f;
        pourcentage_max_population = 1.0f;
        pourcentage_capacite_sante = 1.0f;
        pourcentage_demande_electrique = 1.0f;
        pourcentage_gain_argent = 1.0f;
        pourcentage_besoin_sante = 1.0f;
        pourcentage_augmente_pop = 1.0f;
        pourcentage_production_eolienne = 1.0f;
        pourcentage_prix_du_charbon = 1.0f;
        les_batiment = new List<int>();
        Nuke = new List<GameObject>();
        prix_ville = 300;
        prix_eolienne = 300;
        prix_clinique = 300;
        prix_ecole = 500;
        prix_usine_charbon = 500;
        prix_nuke = 2500;
        prix_du_charbon = 50;
        prix_lycee = 700;
        prix_universite = 2000;
        prix_maison_sante = 500;
        prix_hopital = 2000;
        periode = 0;
        du_vent = true;
        point_crise = new bool[5] {false, false, false, false, false};
        point_crise_max = 5;
        tour_crise = 0;
        les_crises = new List<Crise>();
        besoin_sante = population / 5;
        les_crises_active = new List<Crise>();
        recherche = new bool[8] {false, false, false, false, false, false, false, false};
        cour_elec = 100;
        cour_sante = 10;
        pop_acheter = 0;
        elec_acheter = 0;
        sante_acheter = 0;
        immigration_cour = 5;
        temps_stabilite = 0;
    }


    public static void prochain_tour() // Fonction que l'on lance quand on change de tour
    {
        bouton_exit.GetComponent<Pause>().autorisation = false;
        relai.BroadcastMessage("inapuyable");
        tour += 1;
        relai.efface();
        relai.desactivation();
        calcul_bat();
        fond_tour_apres.GetComponent<TansitionTour>().transition();
    }


    public static void tour_prochaine_crise()
    {
        tour_crise = Random.Range(tour + 2, tour + periode + 3); //sa prend pas la valeur max donc +3 vu que tour est a + 2
    }

    public static void calcul_bat() // calcul le resultat des batiments sur les différentes stats et les mets a jour 
    {
        int charbon = 0;
        int production_electrique_brut = 0;
        int max_population_brut = 100;
        int capacite_sante_brut = 0;
        int demande_electrique_brut = 0;
        int taux_education_brut = 0;
        bool il_y_a_eo = false;
        bool il_y_a_nuke = false;
        foreach (int i in les_batiment.ToList())
        {
            if (i == 1) // ville de base
            {
                max_population_brut += 1000;
                demande_electrique_brut += 1;
                continue;
            }
            else if (i == 2) // ville n2
            { 
                max_population_brut += 3000;
                demande_electrique_brut += 4;
                continue;
            }
            else if (i == 3) // ville n3
            {
                max_population_brut += 10000;
                demande_electrique_brut += 8;
                continue;
            }
            else if (du_vent == true)// production electrique éolienne
            {
                if (i == 101)
                {
                    production_electrique_brut += (int)(4 * pourcentage_production_eolienne);
                    il_y_a_eo = true;
                    continue;
                }
                else if (i == 102)
                {
                    production_electrique_brut += (int)(8 * pourcentage_production_eolienne);
                    il_y_a_eo = true;
                    continue;
                }
                else if (i == 103)
                {
                    production_electrique_brut += (int)(13 * pourcentage_production_eolienne);
                    il_y_a_eo = true;
                    continue;
                }
                else if (i == 104)
                {
                    production_electrique_brut += (int)(20 * pourcentage_production_eolienne);
                    il_y_a_eo = true;
                    continue;
                }
            }
            if (i == 111) // charbon
            {
                production_electrique_brut += 3;
                charbon += 1;
            }
            else if (i == 112)
            {
                production_electrique_brut += 7;
                charbon += 2;
            }
            else if (i == 113)
            {
                production_electrique_brut += 15;
                charbon += 4;
            }
            else if (i == 114)
            {
                production_electrique_brut += 35;
                charbon += 8;
            }
            else if (i == 121) // nucleaire
            {
                production_electrique_brut += 20;
                il_y_a_nuke = true;
            }
            else if (i == 122)
            {
                production_electrique_brut += 40;
                il_y_a_nuke = true;
            }
            else if (i == 123)
            {
                production_electrique_brut += 60;
                il_y_a_nuke = true;
            }
            else if (i == 124)
            {
                production_electrique_brut += 80;
                il_y_a_nuke = true;
            }
            else if (i == 1001) //clinique
            {
                capacite_sante_brut += 500;
                demande_electrique_brut += 1;
            }
            else if (i == 1002)
            {
                capacite_sante_brut += 1000;
                demande_electrique_brut += 3;
            }
            else if (i == 1003)
            {
                capacite_sante_brut += 2000;
                demande_electrique_brut += 5;
            }
            else if (i == 1004)
            {
                capacite_sante_brut += 3000;
                demande_electrique_brut += 8;
            }
            else if (i == 1011) // maison sante
            {
                capacite_sante_brut += 1250;
                demande_electrique_brut += 3;
            }
            else if (i == 1012)
            {
                capacite_sante_brut += 2500;
                demande_electrique_brut += 7;
            }
            else if (i == 1013)
            {
                capacite_sante_brut += 4000;
                demande_electrique_brut += 10;
            }
            else if (i == 1014)
            {
                capacite_sante_brut += 5000;
                demande_electrique_brut += 15;
            }
            else if (i == 1021) // hopital
            {
                capacite_sante_brut += 1750;
                demande_electrique_brut += 10;
            }
            else if (i == 1022)
            {
                capacite_sante_brut += 3000;
                demande_electrique_brut += 15;
            }
            else if (i == 1023)
            {
                capacite_sante_brut += 6000;
                demande_electrique_brut += 20;
            }
            else if (i == 1024)
            {
                capacite_sante_brut += 8000;
                demande_electrique_brut += 30;
            }
            else if (i == 10001) // ecole
            {
                taux_education_brut += 1;
                demande_electrique_brut += 1;
            }
            else if (i == 10002) 
            {
                taux_education_brut += 3;
                demande_electrique_brut += 3;
            }
            else if (i == 10003)
            {
                taux_education_brut += 7;
                demande_electrique_brut += 5;
            }
            else if (i == 10004)
            {
                taux_education_brut += 20;
                demande_electrique_brut += 8;
            }
            else if (i == 10011) // lycee
            {
                taux_education_brut += 7;
                demande_electrique_brut += 3;
            }
            else if (i == 10012)
            {
                taux_education_brut += 20;
                demande_electrique_brut += 5;
            }
            else if (i == 10013)
            {
                taux_education_brut += 32;
                demande_electrique_brut += 8;
            }
            else if (i == 10014)
            {
                taux_education_brut += 50;
                demande_electrique_brut += 13;
            }
            else if (i == 10021) // universite
            {
                taux_education_brut += 20;
                demande_electrique_brut += 10;
            }
            else if (i == 10022)
            {
                taux_education_brut += 60;
                demande_electrique_brut += 15;
            }
            else if (i == 10023)
            {
                taux_education_brut += 90;
                demande_electrique_brut += 20;
            }
            else if (i == 10024)
            {
                taux_education_brut += 150;
                demande_electrique_brut += 30;
            }
        }
        if (du_vent) // Gestion de la crise des eoliennes
        {
            if (il_y_a_eo)
            {
                int compteur = 0; 
                foreach (Crise crise in les_crises.ToList())
                {
                    if (crise.nom != "Vent calme")
                    {
                        compteur += 1;
                    }
                }
                if(compteur == les_crises.Count)
                {
                    Nouvelle_crise("Vent calme", "Il n'y a plus assé de vent pour faire fonctionner les eoliennes", "les eoliennes", 0, "vent");
                }
            }
            else
            {
                foreach (Crise crise in les_crises.ToList())
                {
                    if (crise.nom == "Vent calme")
                    {
                        les_crises.Remove(crise);
                    }
                }
            }
        }
        if (il_y_a_nuke)
        {
            int compteur = 0;
            foreach (Crise crise in les_crises.ToList())
            {
                if(crise.nom != "Incident nucléaire")
                {
                    compteur += 1;
                }
            }
            if (compteur == les_crises.Count)
            {
                Nouvelle_crise("Incident nucléaire", "Une de vos centrale nucléaire a eu un incident la zone est maintenant inexploitable", "centrale nucleaire", 0, "nuke");
            }
        }
        else
        {
            foreach (Crise crise in les_crises.ToList())
            {
                if (crise.nom == "Incident nucléaire")
                {
                    les_crises.Remove(crise);
                }
            }
        }
        argent -= (int)(charbon * prix_du_charbon * pourcentage_prix_du_charbon); // perte d'argent en fonction du charbon
        taux_education = (int)(taux_education_brut * pourcentage_taux_education);
        production_electrique = (int)(production_electrique_brut * pourcentage_production_electrique) + elec_acheter;
        max_population = (int)(max_population_brut * pourcentage_max_population);
        capacite_sante = (int)(capacite_sante_brut * pourcentage_capacite_sante) + sante_acheter;
        demande_electrique = (int)(demande_electrique_brut * pourcentage_demande_electrique);
        besoin_sante = (int)((population / 5)*pourcentage_besoin_sante); // une clinique pour 5 ville
    }
    public static void Affichage_update() // Permet d'afficher les stats
    {
        affichage_pourcent_gain_argent.text = ((int)(pourcentage_gain_argent * 100)).ToString();
        affichage_pourcent_elec.text = ((int)(pourcentage_production_electrique * 100)).ToString() + " | " + ((int)(pourcentage_demande_electrique * 100)).ToString();
        affichage_pourcent_sante.text = ((int)(pourcentage_capacite_sante * 100)).ToString() + " | " + ((int)(pourcentage_besoin_sante * 100)).ToString();
        affichage_pourcent_education.text = ((int)(pourcentage_taux_education * 100)).ToString();
        affichage_pourcent_gain_pop.text = ((int)(pourcentage_augmente_pop * 100)).ToString();
    }
}


[System.Serializable] // Permet d'utiliser les objets de la classe comme des varaibles et donc de les mettres dans des list 
//en tant que composant de la liste
public class Crise
{
    public string nom;
    public string description;
    public string cible;
    public int pourcentage;
    public string unique;
    private int temps;


    public Crise(string un_nom, string descri, string la_cible, int pourcent, string special = null) // Constructeur
    {
        nom = un_nom;
        description = descri;
        cible = la_cible;
        pourcentage = pourcent;
        unique = special;
    }


    public void activation()
    {
        // jouer la cinematique par rapport au nom
        bool normal = true;
        if (unique == null)
        {
            switch (cible)
            {
                case "cour":
                    VariablesEtMethodes.cour_elec = (int)(VariablesEtMethodes.cour_elec * (float)(1f + (float)pourcentage/100));
                    VariablesEtMethodes.cour_sante = (int)(VariablesEtMethodes.cour_sante * (float)(1f + (float)pourcentage / 100));
                    VariablesEtMethodes.immigration_cour = (int)(VariablesEtMethodes.immigration_cour * (float)(1f + (float)pourcentage / 100));
                    pourcentage = Random.Range(20, 21 + VariablesEtMethodes.difficulte * 10);
                    break;
                case "argent":
                    VariablesEtMethodes.argent = (int)(VariablesEtMethodes.argent * (100f - pourcentage) / 100);
                    pourcentage = Random.Range(20, 21 + VariablesEtMethodes.difficulte * 10);
                    break;
                case "gain argent":
                    VariablesEtMethodes.pourcentage_gain_argent -= (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "max population":
                    VariablesEtMethodes.pourcentage_max_population -= (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "prix":
                    VariablesEtMethodes.prix_clinique += pourcentage;
                    VariablesEtMethodes.prix_ecole += pourcentage;
                    VariablesEtMethodes.prix_eolienne += pourcentage;
                    VariablesEtMethodes.prix_hopital += pourcentage;
                    VariablesEtMethodes.prix_lycee += pourcentage;
                    VariablesEtMethodes.prix_maison_sante += pourcentage;
                    VariablesEtMethodes.prix_nuke += pourcentage;
                    VariablesEtMethodes.prix_usine_charbon += pourcentage;
                    VariablesEtMethodes.prix_ville += pourcentage;
                    VariablesEtMethodes.prix_universite += pourcentage;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "population":
                    VariablesEtMethodes.population = (int)(VariablesEtMethodes.population * (100f - pourcentage) / 100);
                    pourcentage = Random.Range(20, 21 + VariablesEtMethodes.difficulte * 10);
                    break;
                case "capacite sante":
                    VariablesEtMethodes.pourcentage_capacite_sante -= (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "besoin sante":
                    VariablesEtMethodes.pourcentage_besoin_sante += (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "taux education":
                    VariablesEtMethodes.pourcentage_taux_education -= (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "demande elec":
                    VariablesEtMethodes.pourcentage_demande_electrique += (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
                case "production elec":
                    VariablesEtMethodes.pourcentage_production_electrique -= (float)pourcentage / 100;
                    temps = VariablesEtMethodes.duree_crise;
                    VariablesEtMethodes.les_crises_active.Add(this);
                    break;
            }
        }
        else if (unique == "vent")
        {
            VariablesEtMethodes.du_vent = false;
            temps = VariablesEtMethodes.duree_crise;
            VariablesEtMethodes.les_crises_active.Add(this);
        }
        else if(unique == "nuke")
        {
            normal = false;
            if (Random.Range(0, 100) <= VariablesEtMethodes.chance_nuke) // chance nuke va permettre de réduire la chance de cette crise grâce à la recherche
            {
                int indice;
                GameObject la_centrale;
                CentraleNuke le_script;
                indice = Random.Range(0, VariablesEtMethodes.Nuke.Count);
                la_centrale = VariablesEtMethodes.Nuke[indice];
                le_script = la_centrale.GetComponent<CentraleNuke>();
                le_script.Incident();
                VariablesEtMethodes.les_crises.Remove(this);
                VariablesEtMethodes.crise_active = this;
            }
        }
        if (normal)
        {
            VariablesEtMethodes.les_crises.Remove(this);
            VariablesEtMethodes.crise_active = this;
            VariablesEtMethodes.pop_up.SetActive(true);
            VariablesEtMethodes.pop_up.BroadcastMessage("activationn");
            VariablesEtMethodes.calcul_bat();
        }
        VariablesEtMethodes.Affichage_update();
    }
    public void decompte() // permet d'annuler les effets de certaines crise ou bout d'un momment defini dans les variables
    {
        if (temps >= 1)
        {
            temps -= 1;
        }
        else
        {
            if (unique == null)
            {
                switch (cible)
                {
                    case "gain argent":
                        VariablesEtMethodes.pourcentage_gain_argent += (float)pourcentage / 100;
                        break;
                    case "max population":
                        VariablesEtMethodes.pourcentage_max_population += (float)pourcentage / 100;
                        break;
                    case "capacite sante":
                        VariablesEtMethodes.pourcentage_capacite_sante += (float)pourcentage / 100;
                        break;
                    case "besoin sante":
                        VariablesEtMethodes.pourcentage_besoin_sante -= (float)pourcentage / 100;
                        break;
                    case "taux education":
                        VariablesEtMethodes.pourcentage_taux_education += (float)pourcentage / 100;
                        break;
                    case "demande elec":
                        VariablesEtMethodes.pourcentage_demande_electrique -= (float)pourcentage / 100;
                        break;
                    case "production elec":
                        VariablesEtMethodes.pourcentage_production_electrique += (float)pourcentage / 100;
                        break;
                    case "prix":
                        VariablesEtMethodes.prix_clinique -= pourcentage;
                        VariablesEtMethodes.prix_ecole -= pourcentage;
                        VariablesEtMethodes.prix_eolienne -= pourcentage;
                        VariablesEtMethodes.prix_hopital -= pourcentage;
                        VariablesEtMethodes.prix_lycee -= pourcentage;
                        VariablesEtMethodes.prix_maison_sante -= pourcentage;
                        VariablesEtMethodes.prix_nuke -= pourcentage;
                        VariablesEtMethodes.prix_usine_charbon -= pourcentage;
                        VariablesEtMethodes.prix_ville -= pourcentage;
                        VariablesEtMethodes.prix_universite -= pourcentage;
                        break;
                }
            }
            else if (unique == "vent")
            {
                VariablesEtMethodes.du_vent = true;
            }
            VariablesEtMethodes.les_crises.Add(this);
            pourcentage = Random.Range(20, 21 + VariablesEtMethodes.difficulte*10);
            VariablesEtMethodes.les_crises_active.Remove(this);
            VariablesEtMethodes.Affichage_update();
        }
    }
}
