using UnityEngine;

public class RelaiMessage : MonoBehaviour 
{
    // Ici on utlise un parent de haut niveau pour renvoyer un message
    // Ce Script est dans un gameobject parent de presque tous les autres
    public GameObject fin;
    public GameObject Victoire;
    public void efface()
    {
        gameObject.BroadcastMessage("desactive");
    }

    public void desactivation()
    {
        gameObject.BroadcastMessage("message_desactivation");
    }
    public void activation()
    {
        gameObject.BroadcastMessage("message_activation");
    }
    public void debloque_nuke()
    {
        gameObject.BroadcastMessage("debloque_nuke");
    }
    public void End()
    {
        fin.SetActive(true);
        gameObject.BroadcastMessage("message_End");
    }

    public void Win()
    {
        Victoire.SetActive(true);
        gameObject.BroadcastMessage("message_Victoire");
    }
}