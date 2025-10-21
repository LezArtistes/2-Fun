using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class OneButtonMenu : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string[] nomDesJeux = { "Jeu 1", "Jeu 2" };
    [SerializeField] private string[] scenesDesJeux = { "Jeu1Scene", "Jeu2Scene" };
    [SerializeField] private Sprite[] imagesDesJeux;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string nomActionMapUI = "UI";
    [SerializeField] private string nomActionBouton = "Submit";

    [Header("UI - Jeu 1")]
    [SerializeField] private TextMeshProUGUI texteJeu1;
    [SerializeField] private Image imageJeu1;
    [SerializeField] private GameObject conteneurJeu1;

    [Header("UI - Jeu 2")]
    [SerializeField] private TextMeshProUGUI texteJeu2;
    [SerializeField] private Image imageJeu2;
    [SerializeField] private GameObject conteneurJeu2;

    [Header("UI - Progression")]
    [SerializeField] private Image barreProgression;
    [SerializeField] private TextMeshProUGUI texteInstruction;

    [Header("Paramètres")]
    [SerializeField] private float tempsPourValider = 1.5f;
    [SerializeField] private float tempsEntreChangements = 0.5f;
    [SerializeField] private Color couleurNormale = Color.white;
    [SerializeField] private Color couleurSelection = Color.yellow;
    [SerializeField] private float echelleNormale = 1f;
    [SerializeField] private float echelleSelection = 1.1f;

    private int indexActuel = 0;
    private float tempsAppui = 0f;
    private float tempsDepuisDernierChangement = 0f;
    private bool boutonEnfonce = false;
    private InputActionMap actionMapUI;
    private InputAction boutonAction;

    void Awake()
    {
        if (inputActions != null)
        {
            actionMapUI = inputActions.FindActionMap(nomActionMapUI);
            if (actionMapUI != null)
            {
                boutonAction = actionMapUI.FindAction(nomActionBouton);
            }
            else
            {
                Debug.LogError($"Action Map '{nomActionMapUI}' introuvable !");
            }
        }
    }

    

    void Start()
    {
        // Configure les textes
        if (texteJeu1 != null && nomDesJeux.Length > 0)
            texteJeu1.text = nomDesJeux[0];

        if (texteJeu2 != null && nomDesJeux.Length > 1)
            texteJeu2.text = nomDesJeux[1];

        // Configure les images
        if (imageJeu1 != null && imagesDesJeux.Length > 0)
            imageJeu1.sprite = imagesDesJeux[0];

        if (imageJeu2 != null && imagesDesJeux.Length > 1)
            imageJeu2.sprite = imagesDesJeux[1];

        // Configure la barre de progression
        if (barreProgression != null)
        {
            barreProgression.fillAmount = 0f;
            barreProgression.color = couleurNormale;
        }

        if (texteInstruction != null)
            texteInstruction.text = "Appui court : Changer | Appui long : Valider";

        // Met à jour l'affichage initial
        MettreAJourSelection();
    }

    void Update()
    {
        if (boutonAction == null) return;

        tempsDepuisDernierChangement += Time.deltaTime;

        if (boutonAction.WasPressedThisFrame())
        {
            boutonEnfonce = true;
            tempsAppui = 0f;
        }

        if (boutonAction.IsPressed() && boutonEnfonce)
        {
            tempsAppui += Time.deltaTime;

            if (barreProgression != null)
            {
                barreProgression.fillAmount = tempsAppui / tempsPourValider;
                barreProgression.color = Color.Lerp(couleurNormale, couleurSelection,
                    tempsAppui / tempsPourValider);
            }

            if (tempsAppui >= tempsPourValider)
            {
                ValiderSelection();
                boutonEnfonce = false;
            }
        }

        if (boutonAction.WasReleasedThisFrame() && boutonEnfonce)
        {
            if (tempsAppui < tempsPourValider && tempsDepuisDernierChangement >= tempsEntreChangements)
            {
                ChangerJeu();
            }

            tempsAppui = 0f;
            boutonEnfonce = false;

            if (barreProgression != null)
            {
                barreProgression.fillAmount = 0f;
                barreProgression.color = couleurNormale;
            }
        }
    }

    void ChangerJeu()
    {
        indexActuel = (indexActuel + 1) % nomDesJeux.Length;
        tempsDepuisDernierChangement = 0f;
        MettreAJourSelection();
    }

    void MettreAJourSelection()
    {
        // Réinitialise tous les conteneurs
        if (conteneurJeu1 != null)
        {
            conteneurJeu1.transform.localScale = Vector3.one * echelleNormale;
            if (imageJeu1 != null)
                imageJeu1.color = Color.white;
        }

        if (conteneurJeu2 != null)
        {
            conteneurJeu2.transform.localScale = Vector3.one * echelleNormale;
            if (imageJeu2 != null)
                imageJeu2.color = Color.white;
        }

        // Met en surbrillance le jeu sélectionné
        GameObject conteneurSelectionne = indexActuel == 0 ? conteneurJeu1 : conteneurJeu2;
        Image imageSelectionnee = indexActuel == 0 ? imageJeu1 : imageJeu2;

        if (conteneurSelectionne != null)
        {
            StartCoroutine(AnimerSelection(conteneurSelectionne.transform));
        }

        if (imageSelectionnee != null)
        {
            imageSelectionnee.color = couleurSelection;
        }
    }

    void ValiderSelection()
    {
        Debug.Log($"Chargement de : {nomDesJeux[indexActuel]}");

        if (indexActuel < scenesDesJeux.Length)
        {
            SceneManager.LoadScene(scenesDesJeux[indexActuel]);
        }
    }

    System.Collections.IEnumerator AnimerSelection(Transform cible)
    {
        float duree = 0.3f;
        float temps = 0f;
        Vector3 echelleInitiale = Vector3.one * echelleNormale;
        Vector3 echelleCible = Vector3.one * echelleSelection;

        while (temps < duree)
        {
            temps += Time.deltaTime;
            float progression = temps / duree;

            // Animation élastique
            float t = Mathf.Sin(progression * Mathf.PI * 0.5f);
            cible.localScale = Vector3.Lerp(echelleInitiale, echelleCible, t);

            yield return null;
        }

        cible.localScale = echelleCible;
    }
}