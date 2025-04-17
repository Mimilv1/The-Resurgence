using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CentraleNuke : MonoBehaviour
{
    // Script attacher a toutes les centrales nucleaires
    private ScriptBatiment bat;
    void Start()
    {
        bat = gameObject.GetComponent<ScriptBatiment>();
        VariablesEtMethodes.Nuke.Add(gameObject);
    }
    public void Incident() // apperler par la crise
    {
        StartCoroutine(animati());
        VariablesEtMethodes.Nuke.Remove(gameObject);
        VariablesEtMethodes.les_batiment.Remove(bat.id);
    }
    IEnumerator animati() // animation avec deplacement de la camera lors de la crise de centrale nucleaire
    {
        CameraDeplacement cameraDeplacement;
        Button button;
        ScriptBatiment scriptBatiment;
        button = gameObject.GetComponent<Button>();
        button.interactable = false;
        scriptBatiment = gameObject.GetComponent<ScriptBatiment>();
        scriptBatiment.desactive();
        cameraDeplacement = Camera.main.GetComponent<CameraDeplacement>();
        cameraDeplacement.peut_bouger = false;
        float x_cible, y_cible, x_base, y_base, distance_x, distance_y, taille;
        x_cible = Camera.main.transform.position.x;
        y_cible = Camera.main.transform.position.y;
        x_base = gameObject.transform.position.x;
        y_base = gameObject.transform.position.y;
        distance_x = (x_cible - x_base)/100;
        distance_y = (y_cible - y_base)/100;
        taille = (Camera.main.orthographicSize - 180)/100;
        for (int i = 0; i<100; i++)
        {
            Camera.main.transform.position -= new Vector3(distance_x, distance_y);
            Camera.main.orthographicSize -= taille;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        VariablesEtMethodes.pop_up.SetActive(true);
        VariablesEtMethodes.pop_up.BroadcastMessage("activationn");
        VariablesEtMethodes.calcul_bat();
        cameraDeplacement.peut_bouger = true;
        VariablesEtMethodes.bouton_exit.GetComponent<Pause>().autorisation = true;
        Destroy(gameObject);
    }
}
