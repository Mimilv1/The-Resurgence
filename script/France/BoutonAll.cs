using UnityEngine;
using UnityEngine.UI;

public class BoutonAll : MonoBehaviour
{
    // Permet de desactiver la possibilite d'appuier sur les bouton grace aux message
    private Button bouton;
    private void Start()
    {
        bouton = gameObject.GetComponent<Button>();
    }
    private void message_desactivation()
    {
        bouton.interactable = false;
    }
    private void message_activation()
    {
        bouton.interactable = true;
    }
}
