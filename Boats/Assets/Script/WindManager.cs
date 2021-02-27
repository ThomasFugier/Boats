using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    private static WindManager _instance;

    public static WindManager Instance { get { return _instance; } }

    [Header("References")]
    public ParticleSystem mainWind;

    [Header("Configuration")]
    [Range(0, 30)]
    public float windForce;
    [Range(0, 359)]
    public float windDirection;

    [Header("Infos")]
    public float windX;
    public float windY;

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

    void Start()
    {
        StartCoroutine(WindUpdate());
    }

    IEnumerator WindUpdate()
    {
        while(true)
        {
            yield return null;

            ParticleSystem.VelocityOverLifetimeModule VOLM = mainWind.velocityOverLifetime;
            ParticleSystem.EmissionModule EM = mainWind.emission;

            if(windDirection >= 0 && windDirection < 90)
            {
                windX = Mathf.Lerp(0, 1, windDirection / 90);
                windY = Mathf.Lerp(1, 0, windDirection / 90);
            }

            else if (windDirection >= 90 && windDirection < 180)
            {
                windX = Mathf.Lerp(1, 0, (windDirection - 90) / 90);
                windY = Mathf.Lerp(0, -1, (windDirection - 90) / 90);
            }

            else if (windDirection >= 180 && windDirection < 270)
            {
                windX = Mathf.Lerp(0, -1, (windDirection - 180) / 90);
                windY = Mathf.Lerp(-1, 0, (windDirection - 180) / 90);
            }

            else if (windDirection >= 270 && windDirection < 360)
            {
                windX = Mathf.Lerp(-1, 0, (windDirection - 270) / 90);
                windY = Mathf.Lerp(0, 1, (windDirection - 270) / 90);
            }

            VOLM.x = windX * windForce;
            VOLM.z = windY * windForce;

            EM.rateOverTime = windForce * 2;
        }
    }
}
