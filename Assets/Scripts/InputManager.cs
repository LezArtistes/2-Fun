using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionReference PlayerOnePress;
    public InputActionReference PlayerOneRelease;

    public InputActionReference PlayerTwoPress;
    public InputActionReference PlayerTwoRelease;
    public ControllerAll playerController;

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
        throw new System.NotImplementedException();
    }
    private void CallPlayerOneRelease(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
    private void CallPlayerTwoPress(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
    private void CallPlayerTwoRelease(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
}
