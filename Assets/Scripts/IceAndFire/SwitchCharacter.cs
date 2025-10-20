using UnityEngine;

public class SquareSwapController : MonoBehaviour, ControllerAll
{
    public GameObject square1;
    public GameObject square2;

    private InputManager inputManager;

    private void Start()
    {
        // Cherche automatiquement l�InputManager dans la sc�ne
        inputManager = Object.FindFirstObjectByType<InputManager>();
        if (inputManager == null)
        {
            Debug.LogError("Aucun InputManager trouv� dans la sc�ne !");
            return;
        }

        // On lie ce contr�leur � l'InputManager
        inputManager.setController(this);
    }

    public void PlayerOnePress()
    {
        SwapSquares();
    }

    public void PlayerOneRelease() { }

    public void PlayerTwoPress()
    {
        SwapSquares();
    }

    public void PlayerTwoRelease() { }

    private void SwapSquares()
    {
        Vector3 tempPos = square1.transform.position;
        square1.transform.position = square2.transform.position;
        square2.transform.position = tempPos;
    }
}
