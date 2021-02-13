using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    [Header("References")]
    public GameplayManager gameplayManager;

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
        StartGameplay();

        if(gameplayManager != this.GetComponent<GameplayManager>())
        {
            gameplayManager = this.GetComponent<GameplayManager>();
        }
    }


    public void StartGameplay()
    {
        this.GetComponent<GameplayManager>().StartFishLoop();
    }
}
