using UnityEngine;
using System;

public class AsteroidCollision : MonoBehaviour
{
    public static event Action<AsteroidCollision> OnAsteroidHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Astéroïde '{name}' has hit the Player");
            if (OnAsteroidHit != null)
            {
                OnAsteroidHit(this);
            }
        }
    }
}
