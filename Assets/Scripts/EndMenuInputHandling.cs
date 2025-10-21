using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndMenuInputHandling : MonoBehaviour
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
        SceneManager.LoadSceneAsync("Menu");
    }

    private void CallPlayerTwoPress(InputAction.CallbackContext ic)
    {
        SceneManager.LoadSceneAsync("Menu");
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
