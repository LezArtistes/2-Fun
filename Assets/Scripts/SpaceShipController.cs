using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipController : MonoBehaviour, ControllerAll
{
    public static event Action<SpaceShipController> OnLostHealth;
    
    public float speedFactor;
    public InputManager inputManager;
    public float speedRotation;
    public float speedMove;
    public GameObject VaisseauBody;
    public GameObject rightMotor;
    public GameObject leftMotor;
    public int startingHealth;
    public int health = 3;

    private void Start()
    {
        speedFactor = 0;
        speedRotation = 100;
        speedMove = 10;
        health = startingHealth;
        inputManager.setController(this);
    }

    private void OnEnable() // Ici on s'abonne à l'évènement OnAsteroidHit
    {
        AsteroidCollision.OnAsteroidHit += TakeDamage;
    }

    private void OnDisable()
    {
        AsteroidCollision.OnAsteroidHit -= TakeDamage;
    }

    private void TakeDamage(AsteroidCollision asteroid) // Et ici on fait ce qu'on veut faire avec l'Asteroid touché
    {
        GetComponent<AudioSource>().Play();
        health--;
        if (health <= 0)
        {
            StaticInfo.pathToBackground = "SpaceMountain/background_blur";
            SceneManager.LoadSceneAsync("FinDePartie");
            health = 0;
        }
        if (OnLostHealth != null)
        {
            OnLostHealth(this);
        }
        Debug.Log("Player knows it has been hit by an asteroid");
    }

    public void PlayerOnePress()
    {
        leftMotor.SetActive(true);
    }
    public void PlayerOneRelease()
    {
        leftMotor.SetActive(false);
    }
    public void PlayerTwoPress()
    {
        rightMotor.SetActive(true);
    }
    public void PlayerTwoRelease()
    {
        rightMotor.SetActive(false);
    }

    void Update()
    {
        float rotationSens = 0;
        if (rightMotor.activeSelf)
        {
            rotationSens += 1;
        }
        if (leftMotor.activeSelf)
        {
            rotationSens -= 1;
        }
        transform.Rotate(0, 0, rotationSens * Time.deltaTime * speedRotation);

        float rotation = transform.localEulerAngles.z - 180;
        float direction = Mathf.Sign(rotation);
        float puissance =  1 - Mathf.Abs(Mathf.Abs(rotation) - 90) / 90;
        Vector3 translation = new Vector3(direction * puissance, 0, 0);
        transform.position += translation * Time.deltaTime * speedMove;

        // Clamp la position pour pas dépasser
        float newX = Mathf.Clamp(transform.position.x, -7.5f, 7.5f);
        Debug.Log($"Voici le new X : {newX}");
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
