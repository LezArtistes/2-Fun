using UnityEngine;
using System;
using LitMotion;
using LitMotion.Extensions;

public class AsteroidCollision : MonoBehaviour
{
    public static event Action<AsteroidCollision> OnAsteroidHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().isTrigger = false;
            LSequence.Create()
                .Join(LMotion.Create(1f, .2f, .4f)
                    .BindToLocalScaleY(transform))
                .Append(LMotion.Create(1f, 0f, .4f)
                    .BindToLocalScaleX(transform))
                .Run();
            if (OnAsteroidHit != null)
            {
                OnAsteroidHit(this);
            }
            Debug.Log($"Astéroïde '{name}' has hit the Player");
        }
    }
}
