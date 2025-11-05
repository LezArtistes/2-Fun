using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private void OnEnable() // Ici on s'abonne à l'évènement OnAsteroidHit
    {
        SpaceShipController.OnLostHealth += CameraShake;
    }

    private void OnDisable()
    {
        SpaceShipController.OnLostHealth -= CameraShake;
    }

    private void CameraShake(SpaceShipController spaceship)
    {
        LSequence.Create()
            .Join(LMotion.Shake.Create(transform.localPosition.x, transform.localPosition.x + 0.5f, 0.3f)
                .WithFrequency(5)
                .BindToLocalPositionX(transform))
            .Join(LMotion.Shake.Create(transform.localPosition.y, transform.localPosition.y + 0.5f, 0.3f)
                .WithFrequency(5)
                .BindToLocalPositionY(transform))
            .Run()
            .AddTo(gameObject);
    }
}
