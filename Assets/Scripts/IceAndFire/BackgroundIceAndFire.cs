using UnityEngine;

public class BackgroundIceAndFire : MonoBehaviour
{
    public Transform cameraTransform; // Référence à la caméra
    public float imageWidth = 19.2f; // Largeur d'une image (en unités monde)

    private Transform[] backgrounds;

    void Start()
    {
        // Récupère les deux images enfants du GameObject
        backgrounds = new Transform[2];
        backgrounds[0] = transform.GetChild(0);
        backgrounds[1] = transform.GetChild(1);
    }

    void Update()
    {
        // Si la caméra dépasse complètement le 1er background
        if (cameraTransform.position.x > backgrounds[1].position.x)
        {
            // Replace le 1er à droite du 2e
            backgrounds[0].position = new Vector3(backgrounds[1].position.x + imageWidth, backgrounds[0].position.y, backgrounds[0].position.z);

            // Inverse les références (le 2 devient le 1)
            Swap();
        }

        // (optionnel) si la caméra recule, on peut aussi gérer l’autre sens :
        else if (cameraTransform.position.x < backgrounds[0].position.x)
        {
            // Replace le 2e à gauche du 1er
            backgrounds[1].position = new Vector3(backgrounds[0].position.x - imageWidth, backgrounds[1].position.y, backgrounds[1].position.z);

            Swap();
        }
    }

    void Swap()
    {
        // Échange les références dans le tableau
        Transform temp = backgrounds[0];
        backgrounds[0] = backgrounds[1];
        backgrounds[1] = temp;
    }
}
