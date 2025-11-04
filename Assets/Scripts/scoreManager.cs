using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public Sprite[] image;
    public GameObject prefabsImage;
    public List<GameObject> scoreImages;
    public int score;
    private float t;

    private void OnEnable()
    {
        SwitchCharacter.EndGame += EndGameElementsHandler;
        SpaceShipController.EndGame += EndGameSpaceShipHandler;
    }

    private void OnDisable()
    {
        SwitchCharacter.EndGame -= EndGameElementsHandler;
        SpaceShipController.EndGame -= EndGameSpaceShipHandler;
    }

    private void EndGameElementsHandler(SwitchCharacter sc)
    {
        Debug.Log("J'ai reçu la demande de traitement de fin de jeu");
        StaticInfo.scoreAfterGame = score;
        if (!PlayerPrefs.HasKey("HighScoreElements"))
        {
            PlayerPrefs.SetInt("HighScoreElements", score);
            StaticInfo.isNewHighScore = true;
        } else
        {
            if (score > PlayerPrefs.GetInt("HighScoreElements"))
            {
                PlayerPrefs.SetInt("HighScoreElements", score);
                StaticInfo.isNewHighScore = true;
            } else
            {
                StaticInfo.isNewHighScore = false;
            }
        }
        Debug.Log("J'ai terminé de traiter la demande !");
    }

    private void EndGameSpaceShipHandler(SpaceShipController sc)
    {
        Debug.Log("J'ai reçu la demande de traitement de fin de jeu");
        StaticInfo.scoreAfterGame = score;
        if (!PlayerPrefs.HasKey("HighScoreSpaceShip"))
        {
            PlayerPrefs.SetInt("HighScoreSpaceShip", score);
            StaticInfo.isNewHighScore = true;
        } else
        {
            if (score > PlayerPrefs.GetInt("HighScoreSpaceShip"))
            {
                PlayerPrefs.SetInt("HighScoreSpaceShip", score);
                StaticInfo.isNewHighScore = true;
            } else
            {
                StaticInfo.isNewHighScore = false;
            }
        }
        Debug.Log("J'ai terminé de traiter la demande !");
    }

    private void Update()
    {
        t += Time.deltaTime;
        score = (int)t;
        float scoreTempo = score;
        UpdateImages();
    }

    private void UpdateImages()
    {
        int scoreUse = score;
        int ind = 0;
        while (scoreUse > 0)
        {
            int value = scoreUse % 10;
            if (ind >= scoreImages.Count)
            {
                scoreImages.Add(Instantiate(prefabsImage, transform));
                scoreImages[ind].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    -100 * 1.5f * ind -35 * 1.5f, 
                    -105
                );
            }
            scoreImages[ind].GetComponent<ScoreImage>().setSprite(image[value]);
            scoreUse /= 10;
            ind++;
        }
    }
}
