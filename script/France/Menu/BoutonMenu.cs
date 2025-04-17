using UnityEngine;

public class BoutonMenu : MonoBehaviour
{
    // Action quand on appuie sur le bouton echap
    public GameObject le_menu;
    public RelaiMessage relai;
    public CameraDeplacement la_cam;
    public void Changement()
    {
        relai.efface();
        if (!le_menu.activeSelf)
        {
            relai.BroadcastMessage("inapuyable");
        }
        else
        {
            relai.BroadcastMessage("apuyable");
        }
        le_menu.SetActive(!le_menu.activeSelf);
        la_cam.peut_bouger = !la_cam.peut_bouger;
    }
}
