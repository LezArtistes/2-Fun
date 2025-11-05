using UnityEngine;

public class AudioError : MonoBehaviour
{
    private void OnEnable()
    {
        HitWall.WallHit += PlaySound;
    }

    private void OnDisable()
    {
        HitWall.WallHit -= PlaySound;
    }

    private void PlaySound(HitWall wallHit)
    {
        GetComponent<AudioSource>().Play();
    }
}
