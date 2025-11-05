using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndMenuInputHandling : MonoBehaviour
{
    public InputActionReference UI;
    public float timeToValidate = 1f;

    private AudioSource sfxProgressBar;
    private float timeButtonHeld = 0f;
    private bool isLocallyPressed = false;
    private InputAction inputAction;
    private MotionHandle handle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfxProgressBar = GetComponent<AudioSource>();
        inputAction = UI.action;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAction == null) return;

        if (inputAction.WasPressedThisFrame())
        {
            sfxProgressBar.Play();
            handle = LMotion.Create(0f, 2f, timeToValidate).BindToPitch(sfxProgressBar);
            timeButtonHeld = Time.time;
            isLocallyPressed = true;
        }

        if (inputAction.IsPressed() && isLocallyPressed && Time.time - timeButtonHeld > timeToValidate)
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        if (inputAction.WasReleasedThisFrame())
        {
            sfxProgressBar.Stop();
            handle.TryCancel();
            isLocallyPressed = false;
        }
    }
}
