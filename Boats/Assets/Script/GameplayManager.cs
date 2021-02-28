using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Informations")]
    public bool isInGameplay;

    [Header("Main Mode")]
    public int duration = 60;

    [Header("Stats")]
    public int moneyPerFish = 10;

    [Header("Fishzones")]
    public GameObject prefab_Fishzone;
    public List<Fishzone> fishzones = new List<Fishzone>();
    public int limitFishzoneSpawnX;
    public int limitFishzoneSpawnZ;

    private float mainGameplayTimer = 0;
    private Coroutine coroutine_FishLoop;

    public void Update()
    {
        if(mainGameplayTimer > 0)
        {
            mainGameplayTimer -= Time.deltaTime;
        }

        if(mainGameplayTimer <= 0)
        {
            if(isInGameplay)
            {
                FinishGame();
            }
        }
    }

    public void StartFishLoop()
    {
        coroutine_FishLoop = StartCoroutine(FishLoop());
    }

    public void StopFishLoop()
    {
        StopCoroutine(coroutine_FishLoop);
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

    public void EnablePlayerInputs()
    {
        PlayerManager[] PM = GameObject.FindObjectsOfType<PlayerManager>();
        
        foreach(PlayerManager pm in PM)
        {
            pm.inputsLocked = false;
        }
    }

    public void DisablePlayerInputs() 
    {
        PlayerManager[] PM = GameObject.FindObjectsOfType<PlayerManager>();

        foreach (PlayerManager pm in PM)
        {
            pm.inputsLocked = true;
        }
    }

    public void FinishGame()
    {
        isInGameplay = false;

        DisablePlayerInputs();
        StopFishLoop();
    }

    public void StartGame()
    {
        StartFishLoop();
        WaterManager.Instance.gameObject.GetComponent<Animator>().enabled = true;
    }

    public void StartCountdown(int duration)
    {
        mainGameplayTimer = duration;
        isInGameplay = true;
        StartGame();
    }

    public float GetTimeLeft()
    {
        return mainGameplayTimer;
    }
}
