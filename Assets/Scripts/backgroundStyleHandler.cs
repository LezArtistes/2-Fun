using UnityEngine;
using UnityEngine.UI;

public class backgroundStyleHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string pathToBackground = "";
        switch (StaticInfo.lastGamePlayed)
        {
            case (int)StaticInfo.Games.SPACESHIP:
                pathToBackground = "SpaceMountain/background_blur";
                break;
            case (int)StaticInfo.Games.ELEMENTS:
                pathToBackground = "SpriteIceAndFire/background_icenfire";
                break;
            default:
                Debug.Log("Game not implemented yet !");
                break;
        }
        Sprite sprite = Resources.Load<Sprite>(pathToBackground);
        if (sprite == null)
        {
            Debug.Log("Cheming vers le background non valide ! Voici le path : " + pathToBackground);
        }
        GetComponent<Image>().sprite = sprite;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
