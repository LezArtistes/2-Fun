using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Ast�ro�de '{name}' est entr� en collision avec le joueur !");
        }
    }
}
