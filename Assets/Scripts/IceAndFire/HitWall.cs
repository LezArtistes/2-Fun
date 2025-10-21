using UnityEngine;

public class HitWall : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision d�tect�e avec {collision.gameObject.name}");
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
                Debug.Log($"{playerTag} a d�truit {wallTag} !");
                Destroy(other.gameObject); // on d�truit le mur correspondant
            }
            else
            {
                Debug.Log($"Vous avez perdu une vie !");
            }
        }
    }

    // V�rifie si le joueur correspond au mur
    private bool IsMatchingWall(string playerTag, string wallTag)
    {
        return (playerTag == "PlayerFire" && wallTag == "FireWall") ||
               (playerTag == "PlayerIce" && wallTag == "IceWall") ||
               (playerTag == "PlayerGreen" && wallTag == "GreenWall");
    }

    // V�rifie si l�objet touch� est un mur
    private bool IsWallTag(string wallTag)
    {
        return wallTag == "FireWall" || wallTag == "IceWall" || wallTag == "GreenWall";
    }
}
