using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerManager thisPlayer;
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        { 
            thisPlayer.BouncePlayer(collision.gameObject.transform.parent.GetComponent<PlayerManager>(), thisPlayer.GetComponent<Rigidbody>().velocity, Mathf.Clamp(Vector3.Normalize(collision.impulse).magnitude, 0 ,1));
        }
    }
}
