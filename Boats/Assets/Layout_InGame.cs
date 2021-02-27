using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout_InGame : MonoBehaviour
{
    public PlayerDisplayInGame playerDisplayerInGame_player1;
    public PlayerDisplayInGame playerDisplayerInGame_player2;
    public PlayerDisplayInGame playerDisplayerInGame_player3;
    public PlayerDisplayInGame playerDisplayerInGame_player4;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetupTwoPlayers()
    {
        playerDisplayerInGame_player1.gameObject.SetActive(true);
        playerDisplayerInGame_player2.gameObject.SetActive(true);
        playerDisplayerInGame_player3.gameObject.SetActive(false);
        playerDisplayerInGame_player4.gameObject.SetActive(false);
    }

    public void SetupThreePlayers()
    {
        playerDisplayerInGame_player1.gameObject.SetActive(true);
        playerDisplayerInGame_player2.gameObject.SetActive(true);
        playerDisplayerInGame_player3.gameObject.SetActive(true);
        playerDisplayerInGame_player4.gameObject.SetActive(false);
    }

    public void SetupFourPlayers()
    {
        playerDisplayerInGame_player1.gameObject.SetActive(true);
        playerDisplayerInGame_player2.gameObject.SetActive(true);
        playerDisplayerInGame_player3.gameObject.SetActive(true);
        playerDisplayerInGame_player4.gameObject.SetActive(true);
    }
}
