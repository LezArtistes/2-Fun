using UnityEngine;
using UnityEngine.InputSystem;

public class InputQuit : MonoBehaviour
{
    public InputActionReference playerOnePress;
    public InputActionReference playerTwoPress;
    void OnEnable()
    {
        playerOnePress.action.performed += CallPlayerOnePress;
        playerTwoPress.action.performed += CallPlayerTwoPress;
    }

    void OnDisable()
    {
        playerOnePress.action.performed -= CallPlayerOnePress;
        playerTwoPress.action.performed -= CallPlayerTwoPress;
    }

    private void CallPlayerOnePress(InputAction.CallbackContext ic)
    {
        Application.Quit();
    }

    private void CallPlayerTwoPress(InputAction.CallbackContext ic)
    {
        Application.Quit();
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
