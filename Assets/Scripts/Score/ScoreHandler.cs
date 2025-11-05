using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public GameObject highScoreObject;
    public GameObject currentScoreObject;
    public GameObject newHighScoreObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int highScore = 0;
        switch (StaticInfo.lastGamePlayed)
        {
            case (int)StaticInfo.Games.SPACESHIP:
                highScore = PlayerPrefs.GetInt("HighScoreSpaceShip");
                break;
            case (int)StaticInfo.Games.ELEMENTS:
                highScore = PlayerPrefs.GetInt("HighScoreElements");
                break;
            default:
                Debug.Log("Game not implemented yet !");
                break;
        }
        highScoreObject.GetComponent<TextMeshProUGUI>().text = "" + highScore;
        currentScoreObject.GetComponent<TextMeshProUGUI>().text = "" + StaticInfo.scoreAfterGame;
        if (StaticInfo.isNewHighScore)
        {
            newHighScoreObject.SetActive(true);
        } else
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -200, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
