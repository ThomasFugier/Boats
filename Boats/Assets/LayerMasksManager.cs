using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMasksManager : MonoBehaviour
{
    private static LayerMasksManager _instance;

    public static LayerMasksManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public LayerMask LM_WaterSurfaceRaycasting;
}
