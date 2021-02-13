using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishzone : MonoBehaviour
{
    [Header("Gameplay Configuration")]
    public int fishAmount = 10;
    public float requiredTimeToCatch = 2;

    [Header("Technical Configuration")]
    public float offsetFromSurface;
    [Header("Render")]
    public GameObject FX;
    public float FX_offsetFromCamera;

    [HideInInspector]
    public bool canBeFished = true;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateHeight();
    }

    public void UpdateHeight()
    {
        RaycastHit hit;

        Ray ray = new Ray(new Vector3(this.transform.position.x, 500, this.transform.position.z), Vector3.down * 1000);

        if (Physics.Raycast(ray, out hit, 1000, LayerMasksManager.Instance.LM_WaterSurfaceRaycasting))
        {
            if(hit.collider.gameObject.tag != "WaterCollider")
            {
                DestroyFishZone();
            }

             Vector3 TEMP = this.transform.position;
             TEMP.y = hit.point.y - offsetFromSurface;
             this.transform.position = TEMP;
        }

        Vector3 pos = Camera.main.transform.position;
        Vector3 dir = (this.transform.position - pos).normalized;

        FX.transform.localPosition = Vector3.zero + (dir * FX_offsetFromCamera);
    }

    public void DestroyFishZone()
    {
        PlayerManager[] allPlayers = GameObject.FindObjectsOfType<PlayerManager>();

        foreach(PlayerManager PM in allPlayers)
        {
            if(PM.actualFishzone == this)
            {
                PM.ExitFromFishZone();
            }
        }

        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        FX.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(2);
        GameManager.Instance.gameplayManager.fishzones.Remove(this);
        Destroy(this.gameObject);

    }

    public void OnTriggerEnter(Collider other)
    {
        if(canBeFished)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.transform.parent.transform.parent.gameObject.GetComponent<PlayerManager>().EnterInFishZone(this);
            }
        }
       
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.transform.parent.gameObject.GetComponent<PlayerManager>().ExitFromFishZone();
        }
    }
}
