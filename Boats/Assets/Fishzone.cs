using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishzone : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        UpdateHeight();
    }

    public void UpdateHeight()
    {
        //this.transform.position = WaterManager.Instance.SetOnWaterSurface(this.transform.position) - new Vector3(0, 1, 0);
    }
}
