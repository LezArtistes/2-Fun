using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ControllerAll playerController;
    public InputActionReference PlayerOnePress;
    public InputActionReference PlayerOneRelease;

    public InputActionReference PlayerTwoPress;
    public InputActionReference PlayerTwoRelease;

    public void setController(ControllerAll controller)
    {
        playerController = controller;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        PlayerOnePress.action.performed += CallPlayerOnePress ;
        PlayerOneRelease.action.performed += CallPlayerOneRelease;

        PlayerTwoPress.action.performed += CallPlayerTwoPress;
        PlayerTwoRelease.action.performed += CallPlayerTwoRelease;
    }
    void OnDisable()
    {
        PlayerOnePress.action.performed -= CallPlayerOnePress;
        PlayerOneRelease.action.performed -= CallPlayerOneRelease;

        PlayerTwoPress.action.performed -= CallPlayerTwoPress;
        PlayerTwoRelease.action.performed -= CallPlayerTwoRelease;
    }
    private void CallPlayerOnePress(InputAction.CallbackContext obj)
    {
        playerController.PlayerOnePress();
    }
    private void CallPlayerOneRelease(InputAction.CallbackContext obj)
    {
        playerController.PlayerOneRelease();
    }
    private void CallPlayerTwoPress(InputAction.CallbackContext obj)
    {
        playerController.PlayerTwoPress();
    }
    private void CallPlayerTwoRelease(InputAction.CallbackContext obj)
    {
        playerController.PlayerTwoRelease();
    }
}
