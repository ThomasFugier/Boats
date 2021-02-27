using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    public float flotabilityOffset;

    void Update()
    {
        UpdateHeight();
    }

    public void UpdateHeight()
    {
        RaycastHit hit;

        Ray ray = new Ray(new Vector3(this.transform.position.x, 500, this.transform.position.z), Vector3.down * 1000);
        
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 TEMP = this.transform.position;
            TEMP.y = hit.point.y + flotabilityOffset;
            this.transform.position = TEMP;
        }
    }
}
