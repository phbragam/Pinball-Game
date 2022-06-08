using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathDetectionScript : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform ballSapawnPoint;
    public static int ballCounter = 3;
    public static Action gameOver;
    public static Action ballDead;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>())
        {
            other.gameObject.SetActive(false);
            ballCounter--;
            ballDead?.Invoke();
            if (ballCounter > 0)
            {
                other.gameObject.transform.position = ballSapawnPoint.position;
                other.gameObject.SetActive(true);
            }
            else
            {
                gameOver?.Invoke();
            }
        }
    }
}
