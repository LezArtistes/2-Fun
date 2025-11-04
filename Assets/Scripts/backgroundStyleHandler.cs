using UnityEngine;
using UnityEngine.UI;

public class backgroundStyleHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sprite sprite = Resources.Load<Sprite>(StaticInfo.pathToBackground);
        Debug.Log("Le path pour le Background : " + StaticInfo.pathToBackground);

        GetComponent<Image>().sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
