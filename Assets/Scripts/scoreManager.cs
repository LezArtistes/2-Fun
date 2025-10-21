using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class scoreManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Sprite[] image;
    public GameObject prefabsImage;
    public List<GameObject> scoreImages;
    public int score;
    private float t;

    private void Update()
    {
        t += Time.deltaTime;
        score = (int)t;
        float scoreTempo = score;
        text.text = score.ToString();
        updateImages();
    }

    private void updateImages()
    {
        int scoreUse = score;
        int ind = 0;
        while (scoreUse > 0)
        {
            int value = scoreUse % 10;
            if (ind >= scoreImages.Count)
            {
                scoreImages.Add(Instantiate(prefabsImage, transform));
                scoreImages[ind].GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 * ind, 0);
            }
            scoreImages[ind].GetComponent<ScoreImage>().setSprite(image[value]);
            scoreUse = scoreUse / 10;
            ind++;
        }
    }
}
