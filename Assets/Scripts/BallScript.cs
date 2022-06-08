using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour
{
    [SerializeField] float nonRealisticForce;
    public static Action<int> hitSomething;
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
            hitSomething?.Invoke(10);
        }
    }

}
