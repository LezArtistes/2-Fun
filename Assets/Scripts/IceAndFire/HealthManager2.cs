using LitMotion;
using LitMotion.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager2 : MonoBehaviour
{
    public Sprite[] sprites;

    private void OnEnable()
    {
        SwitchCharacter.RipBozo += LoseHealth;
    }

    private void OnDisable()
    {
        SwitchCharacter.RipBozo -= LoseHealth;
    }
    
    private void LoseHealth(SwitchCharacter switchCharacter)
    {
        GetComponent<Image>().sprite = sprites[switchCharacter.health];
        LMotion.Punch.Create(transform.localPosition.x, transform.localPosition.x + 15f, .8f)
            .WithEase(Ease.OutSine)
            .BindToLocalPositionX(transform)
            .AddTo(gameObject);
        Debug.Log("GUI has been updated");
    }
}
