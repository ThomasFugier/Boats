using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    [Header("Input Keys")]
    [Space]
    public string playerDenomination = "Controller";
    public string xJoystick = "Joystick_X";
    public string yJoystick = "Joystick_Y";
    public string mainInput = "Main";

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

    public float GetJoystickAxis_X(PlayerIndex playerIndex)
    {
        string i = "";

        switch(playerIndex)
        {
            case PlayerIndex.Player1:
                i = "1";
                break;

            case PlayerIndex.Player2:
                i = "2";
                break;

            case PlayerIndex.Player3:
                i = "3";
                break;

            case PlayerIndex.Player4:
                i = "4";
                break;
        }

        return Input.GetAxis((playerDenomination + i + "_" + xJoystick));
    }

    public float GetJoystickAxis_Y(PlayerIndex playerIndex)
    {
        string i = "";

        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                i = "1";
                break;

            case PlayerIndex.Player2:
                i = "2";
                break;

            case PlayerIndex.Player3:
                i = "3";
                break;

            case PlayerIndex.Player4:
                i = "4";
                break;
        }

        return Input.GetAxis((playerDenomination + i + "_" + yJoystick));
    }

    public bool GetJoystickButton_Main(PlayerIndex playerIndex)
    {
        string i = "";

        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                i = "1";
                break;

            case PlayerIndex.Player2:
                i = "2";
                break;

            case PlayerIndex.Player3:
                i = "3";
                break;

            case PlayerIndex.Player4:
                i = "4";
                break;
        }

        return Input.GetButton((playerDenomination + i + "_" + mainInput));
    }
}
