using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Stats")]
    public int moneyPerFish = 10;

    [Header("Fishzones")]
    public GameObject prefab_Fishzone;
    public List<Fishzone> fishzones = new List<Fishzone>();
    public int limitFishzoneSpawnX;
    public int limitFishzoneSpawnZ;

    public void StartFishLoop()
    {
        StartCoroutine(FishLoop());
    }

    IEnumerator FishLoop()
    {
        while(true)
        {
            yield return null;

            if(fishzones.Count < 10)
            {
                SpawnFishzone();
            }
        }
    }

    public void SpawnFishzone()
    {
        GameObject newFishzone = Instantiate(prefab_Fishzone, this.transform);
        newFishzone.transform.position = new Vector3(Random.Range(-limitFishzoneSpawnX, limitFishzoneSpawnX), 10, Random.Range(-limitFishzoneSpawnZ, limitFishzoneSpawnZ));
        fishzones.Add(newFishzone.GetComponent<Fishzone>());
    }
}
