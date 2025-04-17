using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Utilisation du bouton pause avec echap
    private Button le_bouton;
    public string touche;
    public bool autorisation;
    private void Start()
    {
        le_bouton = gameObject.GetComponent<Button>();
    }
    void Update()
    {
        if (Input.GetKeyDown(touche) && autorisation)
        {
            FadeColor(le_bouton.colors.pressedColor); // permet de faire l'animation de changement de couleur meme si on appuie pas rellement sur le bouton
            le_bouton.onClick.Invoke();
        }
        else if (Input.GetKeyUp(touche))
        {
            FadeColor(le_bouton.colors.normalColor);
        }
    }
    void FadeColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, le_bouton.colors.fadeDuration, true, true);
    }
}
