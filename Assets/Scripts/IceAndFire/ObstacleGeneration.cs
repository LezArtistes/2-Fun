using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    public float speed;
    public GameObject[] obs;
    public int[] poidsObs;
    private List<int> vraipoidsObs = new List<int>();
    public GameObject instancePosition;
    public float ecartObs;
    private Vector3 lastPosition = Vector3.zero;
    private List<GameObject> obstaclesActif = new List<GameObject>();
    public float difficultyIncreaseTimeInterval;
    private float timeLastDifficultyIncrease;

    private void Start()
    {
        for (int i = 0; i < poidsObs.Length; i++)
        {
            vraipoidsObs.Add(poidsObs[i]);
        }

        // Init the fist timestamp for the difficulty increase
        timeLastDifficultyIncrease = Time.time;
    }
    private void Update()
    {
        float distance = Vector3.Distance(instancePosition.transform.position, lastPosition);
        if (distance > ecartObs)
        {
            genere();
            lastPosition = instancePosition.transform.position;
        }
        instancePosition.transform.Translate(Vector3.right * Time.deltaTime * speed);

        // Update speed every difficultyIncreaseTimeInterval seconds
        if (Time.time - timeLastDifficultyIncrease >= difficultyIncreaseTimeInterval)
        {
            speed += .5f;
            timeLastDifficultyIncrease = Time.time;
        }
    }
    private void genere()
    {
        int alea = Random.Range(0, vraipoidsObs.Sum());
        float value = 0;
        GameObject obsAlea = obs[0];
        for (int i = 0; i < vraipoidsObs.Count; i++) 
        {
            value += vraipoidsObs[i];
            if (alea < value)
            {
                obsAlea = obs[i];
                UpdatePoids(i);
                break;
            }
        }

        // Génération d'un nouveau mur  
        obstaclesActif.Add(Instantiate(obsAlea));
        Vector3 newPosition = instancePosition.transform.position;
        newPosition.x = newPosition.x + 10; // Mur placé au bout de l'écran
        obstaclesActif.Last().transform.position = newPosition;
    }

    private void UpdatePoids(int ind)
    {
        for (int i = 0;i < poidsObs.Length;i++)
        {
            if (i == ind)
            {
                vraipoidsObs[i] -= (int)(vraipoidsObs[i] * (20f / 100f));
            }
            else
            {
                vraipoidsObs[i] += (int)(poidsObs[i] * (10f / 100f));
            }
        }
        

    }

    public void suppAllObs()
    {
        for (int i = 0;i < obstaclesActif.Count; i++)
        {
            Destroy(obstaclesActif[i]);
            obstaclesActif.Free();
        }
    }
}
