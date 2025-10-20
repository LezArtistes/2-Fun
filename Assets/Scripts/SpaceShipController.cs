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
    public GameObject rightMotor;
    public GameObject leftMotor;
    public int health = 3;

    private void Start()
    {
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
        Debug.Log("Player knows it has been hit by an asteroid");
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene("FinDePartie");
            health = 0;
        }
        if (OnLostHealth != null)
        {
            OnLostHealth(this);
        }
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

    private void Update()
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
    }
}
