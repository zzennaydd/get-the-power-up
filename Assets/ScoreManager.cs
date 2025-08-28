using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public float timeElapsed = 0f;
    public int score = 0;
    public int scorePerSecond = 10;
    private int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore",0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;
        score = Mathf.FloorToInt(timeElapsed * scorePerSecond);
        scoreText.text = "Score: " + score.ToString();
     
    }
    public void CheckHighScore()
    {
       if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore",highScore);
            PlayerPrefs.Save();

            highScoreText.text = "High Score: " + highScore.ToString();
            Debug.Log("NEW HIGH SCORE");
        }
    }
}
