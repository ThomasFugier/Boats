using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    [Header("Informations")]
    public int playerNumber;

    [Header("References")]
    public MainMenu mainMenu;
    public GameplayManager gameplayManager;
    public Layout_InGame layout_InGame;
    public ColorDefinitions colorDefinitions;

    [Header("Prefabs")]
    public GameObject player;

    [Header("Players")]
    public PlayerManager player1;
    public PlayerManager player2;
    public PlayerManager player3;
    public PlayerManager player4;

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
        if(gameplayManager != this.GetComponent<GameplayManager>())
        {
            gameplayManager = this.GetComponent<GameplayManager>();
        }
    }


    public void StartGameplay()
    {
        GeneratePlayers();
        this.GetComponent<GameplayManager>().StartFishLoop();
    }

    public void GeneratePlayers()
    {
        playerNumber = 0;

        if(mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerJoined_1)
        {
            GameObject player1 = Instantiate(player, Vector3.zero, Quaternion.identity);

            if(mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerIsAI_1 == false)
            {
                player1.name = "Player 1 | Human";
                player1.GetComponent<PlayerManager>().playerIndex = PlayerIndex.Player1;
            }

            else
            {
                player1.name = "Player 1 | AI";
                player1.GetComponent<PlayerManager>().playerIndex = PlayerIndex.AI;
            }

            this.player1 = player1.GetComponent<PlayerManager>();
            layout_InGame.playerDisplayerInGame_player1.linkedPlayer = player1.GetComponent<PlayerManager>();

            playerNumber++;
        }

        if (mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerJoined_2)
        {
            GameObject player2 = Instantiate(player, Vector3.zero, Quaternion.identity);

            if (mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerIsAI_2 == false)
            {
                player2.name = "Player 2 | Human";
                player2.GetComponent<PlayerManager>().playerIndex = PlayerIndex.Player2;
            }

            else
            {
                player2.name = "Player 2 | AI";
                player2.GetComponent<PlayerManager>().playerIndex = PlayerIndex.AI;
            }

            this.player2 = player2.GetComponent<PlayerManager>();
            layout_InGame.playerDisplayerInGame_player2.linkedPlayer = player2.GetComponent<PlayerManager>();

            playerNumber++;
        }

        if (mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerJoined_3)
        {
            GameObject player3 = Instantiate(player, Vector3.zero, Quaternion.identity);

            if (mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerIsAI_3 == false)
            {
                player3.name = "Player 3 | Human";
                player3.GetComponent<PlayerManager>().playerIndex = PlayerIndex.Player3;
            }

            else
            {
                player3.name = "Player 3 | AI";
                player3.GetComponent<PlayerManager>().playerIndex = PlayerIndex.AI;
            }

            this.player3 = player3.GetComponent<PlayerManager>();
            layout_InGame.playerDisplayerInGame_player3.linkedPlayer = player3.GetComponent<PlayerManager>();

            playerNumber++;
        }

        if (mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerJoined_4)
        {
            GameObject player4 = Instantiate(player, Vector3.zero, Quaternion.identity);

            if (mainMenu.panel_playerSelection.GetComponent<PlayerSelection>().playerIsAI_4 == false)
            {
                player4.name = "Player 4 | Human";
                player4.GetComponent<PlayerManager>().playerIndex = PlayerIndex.Player4;
            }

            else
            {
                player4.name = "Player 4 | AI";
                player4.GetComponent<PlayerManager>().playerIndex = PlayerIndex.AI;
            }

            this.player4 = player4.GetComponent<PlayerManager>();
            layout_InGame.playerDisplayerInGame_player4.linkedPlayer = player4.GetComponent<PlayerManager>();
            playerNumber++;
        }

        switch(playerNumber)
        {
            case 2:
                mainMenu.panel_InGame.GetComponent<Layout_InGame>().SetupTwoPlayers();
                break;

            case 3:
                mainMenu.panel_InGame.GetComponent<Layout_InGame>().SetupThreePlayers();
                break;

            case 4:
                mainMenu.panel_InGame.GetComponent<Layout_InGame>().SetupFourPlayers();
                break;

        }
    }
}
