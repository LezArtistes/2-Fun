using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitWall : MonoBehaviour
{
    public static event Action<HitWall> WallHit;

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
                if (WallHit != null)
                {
                    WallHit(this);
                }
                Debug.Log($"Vous avez perdu une vie !"); 
                Destroy(other.gameObject); // on d�truit le mur correspondant

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
