using UnityEngine;
using UnityEngine.UI;

public class BoutonRond : MonoBehaviour
{
    // permet de mettre la hitbox des boutons sur les ensroits ou ils ont un canal alpha superieur a 0.1
    void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f; // la valeur du calque alpha minimum est de 0,1 et plus de 0 comme ça la partie du png ou y a rien disparait
    }
}
