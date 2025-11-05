using UnityEngine;
using UnityEngine.UI;

public class ScoreImage : MonoBehaviour
{
    public void setSprite(Sprite image)
    {
        GetComponent<Image>().sprite = image;
    }
}
