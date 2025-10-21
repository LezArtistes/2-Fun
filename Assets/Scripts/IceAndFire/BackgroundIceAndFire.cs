using UnityEngine;

public class BackgroundIceAndFire : MonoBehaviour
{
    public Transform cameraTransform; // R�f�rence � la cam�ra
    public float imageWidth = 19.2f; // Largeur d'une image (en unit�s monde)

    private Transform[] backgrounds;

    void Start()
    {
        // R�cup�re les deux images enfants du GameObject
        backgrounds = new Transform[2];
        backgrounds[0] = transform.GetChild(0);
        backgrounds[1] = transform.GetChild(1);
    }

    void Update()
    {
        // Si la cam�ra d�passe compl�tement le 1er background
        if (cameraTransform.position.x > backgrounds[1].position.x)
        {
            // Replace le 1er � droite du 2e
            backgrounds[0].position = new Vector3(backgrounds[1].position.x + imageWidth, backgrounds[0].position.y, backgrounds[0].position.z);

            // Inverse les r�f�rences (le 2 devient le 1)
            Swap();
        }

        // (optionnel) si la cam�ra recule, on peut aussi g�rer l�autre sens :
        else if (cameraTransform.position.x < backgrounds[0].position.x)
        {
            // Replace le 2e � gauche du 1er
            backgrounds[1].position = new Vector3(backgrounds[0].position.x - imageWidth, backgrounds[1].position.y, backgrounds[1].position.z);

            Swap();
        }
    }

    void Swap()
    {
        // �change les r�f�rences dans le tableau
        Transform temp = backgrounds[0];
        backgrounds[0] = backgrounds[1];
        backgrounds[1] = temp;
    }
}
