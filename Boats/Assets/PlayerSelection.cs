using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [Header("Informations")]
    public bool playerJoined_1;
    public bool playerJoined_2;
    public bool playerJoined_3;
    public bool playerJoined_4;
    public bool playerReady_1;
    public bool playerReady_2;
    public bool playerReady_3;
    public bool playerReady_4;

    [Header("References")]
    public PlayerSlot slot_1;
    public PlayerSlot slot_2;
    public PlayerSlot slot_3;
    public PlayerSlot slot_4;

    [Header("UI References")]
    public GameObject playerMinimumDisplay;
    public GameObject backToMenuButtonBlock;

    public void Update()
    {
        if(InputManager.Instance.GetJoystickButton_FrontLeft(PlayerIndex.Player1) && InputManager.Instance.GetJoystickButton_FrontRight(PlayerIndex.Player1))
        {
            PlayerJoin(PlayerIndex.Player1);
        }

        if (InputManager.Instance.GetJoystickButton_FrontLeft(PlayerIndex.Player2) && InputManager.Instance.GetJoystickButton_FrontRight(PlayerIndex.Player2))
        {
            PlayerJoin(PlayerIndex.Player2);
        }

        if (InputManager.Instance.GetJoystickButton_FrontLeft(PlayerIndex.Player3) && InputManager.Instance.GetJoystickButton_FrontRight(PlayerIndex.Player3))
        {
            PlayerJoin(PlayerIndex.Player3);
        }

        if (InputManager.Instance.GetJoystickButton_FrontLeft(PlayerIndex.Player4) && InputManager.Instance.GetJoystickButton_FrontRight(PlayerIndex.Player4))
        {
            PlayerJoin(PlayerIndex.Player4);
        }

        int i = 0;

        if(playerJoined_1)
        {
            i++;
            backToMenuButtonBlock.SetActive(false);
        }

        else
        {
            backToMenuButtonBlock.SetActive(true);
        }

        if (playerJoined_2)
        {
            i++;
        }

        if (playerJoined_3)
        {
            i++;
        }

        if (playerJoined_4)
        {
            i++;
        }

        if(i > 1)
        {
            playerMinimumDisplay.SetActive(false);
        }

        else
        {
            playerMinimumDisplay.SetActive(true);
        }
    }

    public void PlayerJoin(PlayerIndex playerIndex)
    {
        switch(playerIndex)
        {
            case PlayerIndex.Player1:
                slot_1.PlayerConnect();
                playerJoined_1 = true;
                break;

            case PlayerIndex.Player2:
                slot_2.PlayerConnect();
                playerJoined_2 = true;
                break;

            case PlayerIndex.Player3:
                slot_3.PlayerConnect();
                playerJoined_3 = true;
                break;

            case PlayerIndex.Player4:
                slot_4.PlayerConnect();
                playerJoined_4 = true;
                break;
        }
    }

    public void PlayerDisconnect(PlayerIndex playerIndex)
    {
        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                slot_1.PlayerDisconnect();
                playerJoined_1 = false;
                break;

            case PlayerIndex.Player2:
                slot_2.PlayerDisconnect();
                playerJoined_2 = false;
                break;

            case PlayerIndex.Player3:
                slot_3.PlayerDisconnect();
                playerJoined_3 = false;
                break;

            case PlayerIndex.Player4:
                slot_4.PlayerDisconnect();
                playerJoined_4 = false;
                break;
        }
    }

    public void PlayerReady(PlayerIndex playerIndex)
    {
        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                slot_1.PlayerReady();
                playerReady_1 = true;
                break;

            case PlayerIndex.Player2:
                slot_2.PlayerReady();
                playerReady_2 = true;
                break;

            case PlayerIndex.Player3:
                slot_3.PlayerReady();
                playerReady_3 = true;
                break;

            case PlayerIndex.Player4:
                slot_4.PlayerReady();
                playerReady_4 = true;
                break;
        }
    }

    public void PlayerNotReady(PlayerIndex playerIndex)
    {
        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                slot_1.PlayerNoLongerReady();
                playerReady_1 = false;
                break;

            case PlayerIndex.Player2:
                slot_2.PlayerNoLongerReady();
                playerReady_2 = false;
                break;

            case PlayerIndex.Player3:
                slot_3.PlayerNoLongerReady();
                playerReady_3 = false;
                break;

            case PlayerIndex.Player4:
                slot_4.PlayerNoLongerReady();
                playerReady_4 = false;
                break;
        }
    }
}
