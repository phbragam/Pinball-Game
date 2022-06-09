using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour
{
    [SerializeField] float nonRealisticForce;
    public static Action hitObstacle;
    public static float playerScore = 0f;
    [SerializeField] float obstacleScore;
    [SerializeField] float movingObstacleScore;


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<PalleteReference>() &&
        other.gameObject.GetComponent<PalleteReference>().enabled == true)
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * nonRealisticForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<ObstacleReference>())
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            dir.y = 0f;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * nonRealisticForce);
            playerScore += obstacleScore;
            hitObstacle.Invoke();
        }
        else if ((other.gameObject.GetComponent<MovingObstacleReference>()))
        {
            playerScore += movingObstacleScore;
            hitObstacle.Invoke();
        }
    }

}
