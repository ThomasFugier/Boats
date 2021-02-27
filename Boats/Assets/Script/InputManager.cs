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
    public string secondaryInput = "Secondary";
    public string xButtonInput = "Main";
    public string leftFrontInput = "FrontLeft";
    public string rightFrontInput = "FrontRight";

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

            case PlayerIndex.Anyone:
                if(Input.GetButton(playerDenomination + 1 + "_" + mainInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + mainInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 3 + "_" + mainInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + mainInput))
                {
                    return true;
                }

                break;
        }

        if (playerIndex != PlayerIndex.Anyone)
        {
            return Input.GetButton(playerDenomination + i + "_" + mainInput);
        }

        return false;
    }

    public bool GetJoystickButton_Secondary(PlayerIndex playerIndex)
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

            case PlayerIndex.Anyone:
                if (Input.GetButton(playerDenomination + 1 + "_" + secondaryInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + secondaryInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 3 + "_" + secondaryInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + secondaryInput))
                {
                    return true;
                }
                break;
        }


        if(playerIndex != PlayerIndex.Anyone)
        {
            return Input.GetButton(playerDenomination + i + "_" + secondaryInput);
        }

        return false;
    }

    public bool GetJoystickButton_X(PlayerIndex playerIndex)
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

            case PlayerIndex.Anyone:
                if (Input.GetButton(playerDenomination + 1 + "_" + xButtonInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + xButtonInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 3 + "_" + xButtonInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + xButtonInput))
                {
                    return true;
                }
                break;
        }


        if (playerIndex != PlayerIndex.Anyone)
        {
            return Input.GetButton(playerDenomination + i + "_" + xButtonInput);
        }

        return false;
    }

    public bool GetJoystickButton_FrontLeft(PlayerIndex playerIndex)
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

            case PlayerIndex.Anyone:
                if (Input.GetButton(playerDenomination + 1 + "_" + leftFrontInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + leftFrontInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 3 + "_" + leftFrontInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + leftFrontInput))
                {
                    return true;
                }
                break;
        }

        if (playerIndex != PlayerIndex.Anyone)
        {
            return Input.GetButton(playerDenomination + i + "_" + leftFrontInput);
        }

        return false;
    }

    public bool GetJoystickButton_FrontRight(PlayerIndex playerIndex)
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

            case PlayerIndex.Anyone:
                if (Input.GetButton(playerDenomination + 1 + "_" + rightFrontInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + rightFrontInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 3 + "_" + rightFrontInput))
                {
                    return true;
                }

                if (Input.GetButton(playerDenomination + 2 + "_" + rightFrontInput))
                {
                    return true;
                }
                break;
        }

        if (playerIndex != PlayerIndex.Anyone)
        {
            return Input.GetButton(playerDenomination + i + "_" + rightFrontInput);
        }

        return false;
    }
}
