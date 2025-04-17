using UnityEngine;
using UnityEngine.UI;

public class AffichagePointCrise : MonoBehaviour
{
    // Script attacher a chaque cercle des points de crise
    public int valeur;

    void Update()
    {
        if (VariablesEtMethodes.point_crise[valeur])
        {
            gameObject.GetComponent<Image>().color = new Color32(212, 45, 52, 255);
        }
        else 
        {
            gameObject.GetComponent<Image>().color = new Color32(85, 182, 220, 255);
        }
    }
}
