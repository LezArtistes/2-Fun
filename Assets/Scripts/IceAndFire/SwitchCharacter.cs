using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchCharacter : MonoBehaviour
{
    public static event Action<SwitchCharacter> RipBozo;
    public static event Action<SwitchCharacter> EndGame;

    public int startingHealth = 3;
    public int health;

    public GameObject square1;
    public GameObject square2;
    public GameObject square3; 

    public float simultaneousThreshold = 0.17f;

    private InputSystem_Actions inputActions;

    private float lastPlayerOnePressTime = -1f;
    private float lastPlayerTwoPressTime = -1f;

    private bool playersHidden = false;
    private bool playerOneHeld = false;
    private bool playerTwoHeld = false;

    private void Start()
    {
        health = startingHealth;
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        SetSquareVisible(square3, false);
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.PlayerOnePress.performed += OnPlayerOnePress;
        inputActions.Player.PlayerTwoPress.performed += OnPlayerTwoPress;

        inputActions.Player.PlayerOneRelease.performed += OnPlayerOneRelease;
        inputActions.Player.PlayerTwoRelease.performed += OnPlayerTwoRelease;
        HitWall.WallHit += LostHealth;
    }

    private void OnDisable()
    {
        inputActions.Player.PlayerOnePress.performed -= OnPlayerOnePress;
        inputActions.Player.PlayerTwoPress.performed -= OnPlayerTwoPress;

        inputActions.Player.PlayerOneRelease.performed -= OnPlayerOneRelease;
        inputActions.Player.PlayerTwoRelease.performed -= OnPlayerTwoRelease;

        inputActions.Disable();
        HitWall.WallHit -= LostHealth;
    }

    private void LostHealth(HitWall hit)
    {
        health--;
        if (health <= 0)
        {
            health = 0;
            if (EndGame != null)
            {
                EndGame(this);
            }
            StaticInfo.lastGamePlayed = (int)StaticInfo.Games.ELEMENTS;
            SceneManager.LoadSceneAsync("FinDePartie");
        }
        if (RipBozo != null)
        {
            RipBozo(this);
        }
    }

    private void OnPlayerOnePress(InputAction.CallbackContext context)
    {
        if (playersHidden) return;

        playerOneHeld = true;
        lastPlayerOnePressTime = Time.time;

        if (playerTwoHeld && Mathf.Abs(lastPlayerOnePressTime - lastPlayerTwoPressTime) <= simultaneousThreshold)
        {
            HandleSimultaneousPress();
            return;
        }

        if (square1.transform.position.x < square2.transform.position.x)
        {
            SwapSquares();
            Debug.Log("Player 1 (gauche) a échangé les positions !");
        }
    }

    private void OnPlayerTwoPress(InputAction.CallbackContext context)
    {
        if (playersHidden) return;

        playerTwoHeld = true;
        lastPlayerTwoPressTime = Time.time;

        if (playerOneHeld && Mathf.Abs(lastPlayerTwoPressTime - lastPlayerOnePressTime) <= simultaneousThreshold)
        {
            HandleSimultaneousPress();
            return;
        }

        if (square2.transform.position.x < square1.transform.position.x)
        {
            SwapSquares();
            Debug.Log("Player 2 (gauche) a échangé les positions !");
        }
    }


    private void OnPlayerOneRelease(InputAction.CallbackContext context)
    {
        playerOneHeld = false;
        TryRestoreSquares();
    }

    private void OnPlayerTwoRelease(InputAction.CallbackContext context)
    {
        playerTwoHeld = false;
        TryRestoreSquares();
    }

    private void SwapSquares()
    {
        Vector3 temp = square1.transform.position;
        square1.transform.position = square2.transform.position;
        square2.transform.position = temp;
    }

    private void HandleSimultaneousPress()
    {
        Debug.Log("Appui simultané détecté !");

        GameObject rightmost = (square1.transform.position.x > square2.transform.position.x)
            ? square1
            : square2;

        square3.transform.position = rightmost.transform.position;

        SetSquareVisible(square1, false);
        SetSquareVisible(square2, false);

        SetSquareVisible(square3, true);

        playersHidden = true;
    }

    private void TryRestoreSquares()
    {
        if ((!playerOneHeld || !playerTwoHeld) && playersHidden)
        {
            Debug.Log("L'un des deux joueurs a relaché : restauration des carrés !");
            
            SetSquareVisible(square3, false);
            SetSquareVisible(square1, true);
            SetSquareVisible(square2, true);

            playersHidden = false;
        }
    }

    private void SetSquareVisible(GameObject square, bool visible)
    {
        if (square == null) return;

        var renderers = square.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            r.enabled = visible;

        var colliders = square.GetComponentsInChildren<Collider2D>();
        foreach (var c in colliders)
            c.enabled = visible;
    }
}
