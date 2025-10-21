using UnityEngine;

public class HitWall : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision détectée avec {collision.gameObject.name}");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        string wallTag = other.tag;
        string playerTag = gameObject.tag;

        // Si c'est un mur
        if (IsWallTag(wallTag))
        {
            if (IsMatchingWall(playerTag, wallTag))
            {
                Debug.Log($"{playerTag} a détruit {wallTag} !");
                Destroy(other.gameObject); // on détruit le mur correspondant
            }
            else
            {
                Debug.Log($"Vous avez perdu une vie !");
            }
        }
    }

    // Vérifie si le joueur correspond au mur
    private bool IsMatchingWall(string playerTag, string wallTag)
    {
        return (playerTag == "PlayerFire" && wallTag == "FireWall") ||
               (playerTag == "PlayerIce" && wallTag == "IceWall") ||
               (playerTag == "PlayerGreen" && wallTag == "GreenWall");
    }

    // Vérifie si l’objet touché est un mur
    private bool IsWallTag(string wallTag)
    {
        return wallTag == "FireWall" || wallTag == "IceWall" || wallTag == "GreenWall";
    }
}
