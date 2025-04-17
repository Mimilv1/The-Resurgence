using UnityEngine;
using TMPro;

public class AffihcagePrixUp : MonoBehaviour // Ressemble beaucoup a AfficherText
{
    public ScriptBatiment bat;
    public GameObject le_texte;
    public Color couleur;
    void Start()
    {
        mise_a_jour(); // le texte est mis sur la valeur de l'upgrade
        le_texte.GetComponent<TextMeshProUGUI>().color = couleur;
        Physics.queriesHitTriggers = true;
    }
    private void OnMouseEnter()
    {
        le_texte.SetActive(true);
    }
    private void OnMouseExit()
    {
        le_texte.SetActive(false);
    }
    public void mise_a_jour()
    {
        le_texte.GetComponent<TextMeshProUGUI>().text = bat.GetComponent<ScriptBatiment>().prix_upgrade.ToString();
    }
    private void OnEnable()
    {
        mise_a_jour();
    }
}
