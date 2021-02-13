using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    public enum BendType {Add, Negate};

    void Start()
    {
        WaterManager.Instance.waterZones.Add(this);
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0.5f, 1, 0.8f);
        Gizmos.DrawCube(transform.position + new Vector3(0f,0.3f,0) + new Vector3(0, transform.position.y,0), new Vector3(this.transform.localScale.x, 0.01f, this.transform.localScale.z));
    }
}
