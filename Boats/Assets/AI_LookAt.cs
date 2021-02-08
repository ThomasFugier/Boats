using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_LookAt : MonoBehaviour
{
    public PlayerManager thisPlayer;

    void Start()
    {
        
    }

   
    void Update()
    {
        this.transform.LookAt(thisPlayer.target_AI);
        Vector3 rot = this.transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        rot.y -= 0;
        this.transform.eulerAngles = rot;
    }
}
