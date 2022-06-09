using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ballsText;
    public GameObject retryButton;
    public int totalScore;

    void OnEnable()
    {
        UpdateBallsNumber();
        UpdateScore();

        DeathDetectionScript.gameOver += ActivateRetryButton;
        DeathDetectionScript.ballDead += UpdateBallsNumber;
        BallScript.hitObstacle += UpdateScore;
    }

    void OnDisable()
    {
        DeathDetectionScript.gameOver -= ActivateRetryButton;
        DeathDetectionScript.ballDead -= UpdateBallsNumber;
        BallScript.hitObstacle -= UpdateScore;
    }
    public void ReloadScene()
    {
        DeathDetectionScript.ballCounter = 3;
        BallScript.playerScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateRetryButton()
    {
        retryButton.SetActive(true);
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + BallScript.playerScore;
    }

    public void UpdateBallsNumber()
    {
        ballsText.text = "Balls: " + DeathDetectionScript.ballCounter;
    }

}
