using LitMotion;
using LitMotion.Extensions;
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
            .BindToLocalPositionX(transform);
        Debug.Log("GUI has been updated");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
