using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class EcranFin : MonoBehaviour
{
    public Image fond;
    public TextMeshProUGUI text;
    public GameObject bouton;
    void message_End()
    {
        StartCoroutine(fondu());
    }
    IEnumerator fondu()
    {
        bouton.SetActive(true);
        Button bout = bouton.GetComponent<Button>();
        while (fond.color.a != 1)
        {
            var Couleur = fond.color;
            var Couleur2 = text.color;
            var Couleur3 = bout.image.color;
            Couleur.a += 0.003f;
            Couleur2.a += 0.003f;
            Couleur3.a += 0.003f;
            fond.color = Couleur;
            text.color = Couleur2;
            bout.image.color = Couleur3;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
