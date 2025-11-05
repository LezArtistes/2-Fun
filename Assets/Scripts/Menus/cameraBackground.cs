using UnityEngine;

public class cameraBackground : MonoBehaviour
{
    private float h, s, v;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>(); 
        Color.RGBToHSV(cam.backgroundColor, out h, out s, out v);
        h = Random.Range(0f, 1f);
        cam.backgroundColor = Color.HSVToRGB(h, s, v);
    }

    // Update is called once per frame
    void Update()
    {
        Color.RGBToHSV(cam.backgroundColor, out h, out s, out v);
        h += .2f * Time.deltaTime;
        cam.backgroundColor = Color.HSVToRGB(h, s, v);
    }
}