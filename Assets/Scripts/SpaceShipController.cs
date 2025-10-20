using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class SpaceShipController : MonoBehaviour, ControllerAll
{
    public float speed;
    public GameObject rightMotor;
    public GameObject leftMotor;

    public void PlayerOnePress()
    {
        leftMotor.SetActive(true);
    }
    public void PlayerOneRelease()
    {
        leftMotor.SetActive(false);
    }
    public void PlayerTwoPress()
    {

        rightMotor.SetActive(true);
    }
    public void PlayerTwoRelease()
    {

        rightMotor.SetActive(false);
    }
}
