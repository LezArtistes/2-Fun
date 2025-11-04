using UnityEngine;

public class cameraBackground : MonoBehaviour
{
    private float h, s, v;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = GetComponent<Camera>();
        Color.RGBToHSV(camera.backgroundColor, out h, out s, out v);
        h += .2f * Time.deltaTime;
        camera.backgroundColor = Color.HSVToRGB(h, s, v);
    }
}