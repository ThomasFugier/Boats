using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerManager>().EnterInWater(this.transform.position.y);

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerManager>().ExitFromWater();

        }
    }
}
