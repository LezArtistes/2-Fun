using System;
using UnityEngine;

public class HitWall : MonoBehaviour
{
    public static event Action<HitWall> WallHit;

    private AudioSource sfxMaker;

    private void Start()
    {
        sfxMaker = GetComponent<AudioSource>();
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
                sfxMaker.Play();
                Debug.Log($"{playerTag} a détruit {wallTag} !");
                other.gameObject.GetComponent<Wall>().DestroyItself(true);  // on détruit le mur correspondant
            }
            else
            {
                if (WallHit != null)
                {
                    WallHit(this);
                }
                Debug.Log($"Vous avez perdu une vie !");
                other.gameObject.GetComponent<Wall>().DestroyItself(false); // on détruit le mur correspondant
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
