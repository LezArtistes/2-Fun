using LitMotion;
using LitMotion.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable() // Ici on s'abonne à l'évènement OnAsteroidHit
    {
        SpaceShipController.OnLostHealth += LostHealth;
    }

    private void OnDisable()
    {
        SpaceShipController.OnLostHealth -= LostHealth;
    }

    private void LostHealth(SpaceShipController ship)
    {
        GetComponent<Image>().sprite = sprites[ship.health];
        LMotion.Punch.Create(transform.localPosition.x, transform.localPosition.x + 15f, .8f)
            .WithEase(Ease.OutSine)
            .BindToLocalPositionX(transform)
            .AddTo(gameObject);
        Debug.Log("GUI has been updated");
    }
}
