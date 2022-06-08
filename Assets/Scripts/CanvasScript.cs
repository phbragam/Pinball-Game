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

        DeathDetectionScript.gameOver += ActivateRetryButton;
        DeathDetectionScript.ballDead += UpdateBallsNumber;
    }

    void OnDisable()
    {
        DeathDetectionScript.gameOver -= ActivateRetryButton;
        DeathDetectionScript.ballDead -= UpdateBallsNumber;
    }
    public void ReloadScene()
    {
        DeathDetectionScript.ballCounter = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateRetryButton()
    {
        retryButton.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
    }

    public void UpdateBallsNumber()
    {
        ballsText.text = "Balls: " + DeathDetectionScript.ballCounter;
    }

}
