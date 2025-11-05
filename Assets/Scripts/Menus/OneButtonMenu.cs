using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

public class OneButtonMenu : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string[] nomDesJeux = { "Jeu 1", "Jeu 2" };
    [SerializeField] private string[] scenesDesJeux = { "Jeu1Scene", "Jeu2Scene" };
    [SerializeField] private Sprite[] imagesDesJeux;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string nomActionMapUI = "UI";
    [SerializeField] private string nomActionBouton = "Submit";
    [SerializeField] private GameObject sfxMakerSwitchSound;
    [SerializeField] private GameObject sfxMakerProgressBar;

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

    [Header("Param�tres")]
    [SerializeField] private float tempsPourValider = 1.5f;
    [SerializeField] private float tempsEntreChangements = 0.5f;
    [SerializeField] private Color couleurImageSelectionee = Color.yellow;
    [SerializeField] private Color couleurImageNonSelectionee = Color.yellow;
    [SerializeField] private Color couleurBarreNormale = Color.white;
    [SerializeField] private Color couleurBarrePleine = Color.green;
    [SerializeField] private float echelleNormale = 1f;
    [SerializeField] private float echelleSelection = 1.1f;

    private int indexActuel = 0;
    private float tempsAppui = 0f;
    private float tempsDepuisDernierChangement = 0f;
    private bool boutonEnfonce = false;
    private InputActionMap actionMapUI;
    private InputAction boutonAction;
    private MotionHandle handle;
    private AudioSource sfxSwitch;
    private AudioSource sfxProgressBar;

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
            barreProgression.color = couleurBarreNormale;
        }

        if (texteInstruction != null)
            texteInstruction.text = "Appui court : Changer Appui long : Valider";

        // Configure les SFX
        if (sfxMakerSwitchSound != null)
            sfxSwitch = sfxMakerSwitchSound.GetComponent<AudioSource>();

        if (sfxMakerProgressBar != null)
            sfxProgressBar = sfxMakerProgressBar.GetComponent<AudioSource>();

        // Remettre le choix du dernier jeu 
        indexActuel = StaticInfo.lastGamePlayed;

        // Met � jour l'affichage initial
        MettreAJourSelection();
    }

    void Update()
    {
        if (boutonAction == null) return;

        tempsDepuisDernierChangement += Time.deltaTime;

        if (boutonAction.WasPressedThisFrame())
        {
            sfxProgressBar.Play();
            handle = LMotion.Create(0f, 2f, tempsPourValider).BindToPitch(sfxProgressBar);
            boutonEnfonce = true;
            tempsAppui = 0f;
        }

        if (boutonAction.IsPressed() && boutonEnfonce)
        {
            tempsAppui += Time.deltaTime;

            if (barreProgression != null)
            {
                barreProgression.fillAmount = tempsAppui / tempsPourValider;
                barreProgression.color = Color.Lerp(couleurBarreNormale, couleurBarrePleine,
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
                sfxProgressBar.Stop();
                handle.TryCancel();
                sfxSwitch.Play();
                ChangerJeu();
            }

            tempsAppui = 0f;
            boutonEnfonce = false;

            if (barreProgression != null)
            {
                barreProgression.fillAmount = 0f;
                barreProgression.color = couleurBarreNormale;
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
        // R�initialise tous les conteneurs
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

        // Met en surbrillance le jeu s�lectionn�
        GameObject conteneurSelectionne = indexActuel == 0 ? conteneurJeu1 : conteneurJeu2;
        Image imageSelectionnee = indexActuel == 0 ? imageJeu1 : imageJeu2;
        Image imageNonSelectionnee = indexActuel == 0 ? imageJeu2 : imageJeu1;

        if (conteneurSelectionne != null)
        {
            StartCoroutine(AnimerSelection(conteneurSelectionne.transform));
        }

        if (imageSelectionnee != null)
        {
            imageSelectionnee.color = couleurImageSelectionee;
        }

        if (imageNonSelectionnee != null)
        {
            imageNonSelectionnee.color = couleurImageNonSelectionee;
        }
    }

    void ValiderSelection()
    {
        Debug.Log($"Chargement de : {nomDesJeux[indexActuel]}");

        if (indexActuel < scenesDesJeux.Length)
        {
            SceneManager.LoadSceneAsync(scenesDesJeux[indexActuel]);
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

            // Animation �lastique
            float t = Mathf.Sin(progression * Mathf.PI * 0.5f);
            cible.localScale = Vector3.Lerp(echelleInitiale, echelleCible, t);

            yield return null;
        }

        cible.localScale = echelleCible;
    }
}