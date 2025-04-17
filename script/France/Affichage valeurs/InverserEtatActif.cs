using UnityEngine;

public class InverserEtatActif : MonoBehaviour
{
    // permet d'inverser l'etat d'un gameobject (actif, inactif)
    public GameObject cible;
    public void change()
    {
        cible.SetActive(!cible.activeSelf);
    }
}
