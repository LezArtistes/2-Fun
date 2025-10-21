using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class testRandom : MonoBehaviour
{
    public List<int> Liste100 = new List<int>();
    private List<GameObject> circles = new List<GameObject>();
    public GameObject circle;
    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Liste100.Add(0);
            circles.Add(Instantiate(circle, transform));
            circles[i].transform.position = new Vector3(i,0,0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        int value = Random.Range(0, 100);
        Liste100[value]++;
        circles[value].transform.position = new Vector3(circles[value].transform.position.x, Liste100[value], 0);
    }
}
