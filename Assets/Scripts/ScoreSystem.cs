using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreSystem : MonoBehaviour
{
    public Text highScore_Text;
    public Text score_Text;

    float highScore = 0;
    public float currentScore = 0;

    public float SetCurrentScore
    {
        set
        {
            currentScore = value;
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", 0);
        }
        highScore = PlayerPrefs.GetFloat("HighScore");
        highScore_Text.text = "" + highScore;

        StartCoroutine(ScoreManager());
    }
    IEnumerator ScoreManager()
    {
        while (true)
        {
            if (currentScore > highScore)
            {
                PlayerPrefs.SetFloat("HighScore", currentScore);
                highScore = currentScore;
            }
            score_Text.text = "" + currentScore;
            highScore_Text.text = "" + highScore;
            yield return null;
        }
    }
}
