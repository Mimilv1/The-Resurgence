using UnityEngine;

public class CameraDeplacement : MonoBehaviour // MonoBehaviour veut dire script dans unity en gros
{
    // Script qui s'occupe du deplacement de la camera
    private Camera ma_cam;
    public float vitesse = 2.5f; // vitesse de l'écran qui se déplace
    public float distance_bordure = 1f; // la distance a la quelle la souris fait bouger l'écran
    public float vitesse_molette = 16f; // vitesse de zoom et de dezoom

    public float limit_haute = 700f; // limit de la pos y de la cam
    public float limit_basse = -400f; // limit de la posy de la cam
    public float limit_droite = 1100f; // limit de la pos x de la cam
    public float limit_gauche = -700f; // limit de la pos x de la cam
    public float limit_zoom = 0.5f; // limit de zoom
    public float limit_dezoom = 150f; // limit de dezoom
    public bool peut_bouger = true;

    private void message_End()
    {
        Stop();
    }
    void Start()
    {
        ma_cam = GetComponent<Camera>();
        ma_cam.orthographicSize = limit_dezoom; // On commence avec la cam dézoomer au max
    }
    public void Stop()
    {
        peut_bouger = false;
    }
    public void go()
    {
        peut_bouger = true;
    }


    void Update()
    {
        if (peut_bouger) {
            if ((Input.mousePosition.y >= Screen.height - distance_bordure || Input.GetKey("z")) && transform.position.y < ((limit_haute * limit_dezoom) / ma_cam.orthographicSize))
            {
                transform.position += new Vector3(0, vitesse * Time.deltaTime * ma_cam.orthographicSize);
                /* on ajoute le déplacement vitesse * Time.deltaTime
                   vu que Update est appeler a chaque image mais on ne veut pas que la vitesse de 
                   déplacement de l'écran soit différente en fonction des fps de l'écran
                   plus les fps sont bas plus delta est grand car il mesure le temps entre deux frame */
            }

            if ((Input.mousePosition.y <= distance_bordure || Input.GetKey("s")) && transform.position.y > ((limit_basse * limit_dezoom) / ma_cam.orthographicSize))
            {
                transform.position += new Vector3(0, -(vitesse * Time.deltaTime * ma_cam.orthographicSize));
            }

            if ((Input.mousePosition.x >= Screen.width - distance_bordure || Input.GetKey("d")) && transform.position.x < ((limit_droite * limit_dezoom) / ma_cam.orthographicSize))
            {
                transform.position += new Vector3(vitesse * Time.deltaTime * ma_cam.orthographicSize, 0);
            }

            if ((Input.mousePosition.x <= distance_bordure || Input.GetKey("q")) && transform.position.x > ((limit_gauche * limit_dezoom) / ma_cam.orthographicSize))
            {
                transform.position += new Vector3(-(vitesse * Time.deltaTime * ma_cam.orthographicSize), 0);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if ((scroll != 0f) && (ma_cam.orthographicSize <= limit_dezoom) && (ma_cam.orthographicSize >= limit_zoom))
            {
                ma_cam.orthographicSize -= vitesse_molette * scroll; // on fait un -= pour que le scroll souris soit dans le bon sens
                                                                     // parce que scroll est négatif quand on scrolle vers l'avant 
                if (ma_cam.orthographicSize > limit_dezoom)
                {
                    ma_cam.orthographicSize = limit_dezoom;
                }
                if (ma_cam.orthographicSize <= limit_zoom)
                {
                    ma_cam.orthographicSize = limit_zoom + 0.01f;
                }
            }
        }
    }
}
