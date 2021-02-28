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

    [Header("Spawns")]
    public Transform playerSpawn_1;
    public Transform playerSpawn_2;
    public Transform playerSpawn_3;
    public Transform playerSpawn_4;

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
        if(player1)
        player1.SetPlayerToGameplayMode();
        if(player2)
        player2.SetPlayerToGameplayMode();
        if(player3)
        player3.SetPlayerToGameplayMode();
        if(player4)
        player4.SetPlayerToGameplayMode();

        mainMenu.panel_InGame.GetComponent<Layout_InGame>().SetupPlayerSlots();
        this.GetComponent<GameplayManager>().StartCountdown(this.GetComponent<GameplayManager>().duration);
        this.GetComponent<GameplayManager>().EnablePlayerInputs();
    }

    public void RemovePlayer(PlayerIndex playerIndex)
    {
        switch(playerIndex)
        {
            case PlayerIndex.Player1:
                if(player1)
                {
                    Destroy(player1.gameObject);
                    player1 = null;
                }
               
                break;

            case PlayerIndex.Player2:
                if(player2)
                {
                    Destroy(player2.gameObject);
                    player2 = null;
                }
              
                break;

            case PlayerIndex.Player3:
                if(player3)
                {
                    Destroy(player3.gameObject);
                    player3 = null;
                }

                break;

            case PlayerIndex.Player4:
                if(player4)
                {
                    Destroy(player4.gameObject);
                    player4 = null;
                }
                break;
        }
    }

    public void SetPlayerSpawn(PlayerManager player)
    {
        if(player1 != null)
        {
            if (player == player1)
            {
                player.transform.position = playerSpawn_1.position;
                player.transform.rotation = playerSpawn_1.rotation;
            }
        }
        
        if(player2 != null)
        {
            if (player == player2)
            {
                player.transform.position = playerSpawn_2.position;
                player.transform.rotation = playerSpawn_2.rotation;
            }
        }
       
        if(player3 != null)
        {
            if (player == player3)
            {
                player.transform.position = playerSpawn_3.position;
                player.transform.rotation = playerSpawn_3.rotation;


            }
        }
       
        if(player4 != null)
        {
            if (player == player4)
            {
                player.transform.position = playerSpawn_4.position;
                player.transform.rotation = playerSpawn_4.rotation;
            }
        }
        
    }

    public void GeneratePlayer(PlayerIndex playerIndex, bool AI)
    {
        switch(playerIndex)
        {
            case PlayerIndex.Player1:
                GameObject player1 = Instantiate(player, Vector3.zero, Quaternion.identity);

                if(AI == false)
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

                player1.transform.position = PlayerShowroom.Instance.player1.position;
                player1.transform.parent = PlayerShowroom.Instance.player1;
                player1.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;

            case PlayerIndex.Player2:
                GameObject player2 = Instantiate(player, Vector3.zero, Quaternion.identity);

                if (AI == false)
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
                player2.transform.position = PlayerShowroom.Instance.player2.position;
                player2.transform.parent = PlayerShowroom.Instance.player2;
                player2.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;

            case PlayerIndex.Player3:
                GameObject player3 = Instantiate(player, Vector3.zero, Quaternion.identity);

                if (AI == false)
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

                player3.transform.position = PlayerShowroom.Instance.player3.position;
                player3.transform.parent = PlayerShowroom.Instance.player3;
                player3.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;

            case PlayerIndex.Player4:
                GameObject player4 = Instantiate(player, Vector3.zero, Quaternion.identity);

                if (AI == false)
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
                player4.transform.position = PlayerShowroom.Instance.player4.position;
                player4.transform.parent = PlayerShowroom.Instance.player4;
                player4.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }

       
    }
}
