using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout_InGame : MonoBehaviour
{
    [Header("Players")]
    public PlayerDisplayInGame playerDisplayerInGame_player1;
    public PlayerDisplayInGame playerDisplayerInGame_player2;
    public PlayerDisplayInGame playerDisplayerInGame_player3;
    public PlayerDisplayInGame playerDisplayerInGame_player4;

    [Header("Countdown")]
    public UnityEngine.UI.Text countdown;
    public Color countdownColor_Normal;
    public Color countdownColor_LastSeconds;

    void Start()
    {
        
    }

    void Update()
    {
        SetCountdown((int)GameManager.Instance.GetComponent<GameplayManager>().GetTimeLeft());
    }

    public void SetupPlayerSlots()
    {
        if(GameManager.Instance.player1 != null)
        {
            playerDisplayerInGame_player1.gameObject.SetActive(true);
        }

        else
        {
            playerDisplayerInGame_player1.gameObject.SetActive(false);
        }

        if (GameManager.Instance.player2 != null)
        {
            playerDisplayerInGame_player2.gameObject.SetActive(true);
        }

        else
        {
            playerDisplayerInGame_player2.gameObject.SetActive(false);
        }

        if (GameManager.Instance.player3 != null)
        {
            playerDisplayerInGame_player3.gameObject.SetActive(true);
        }

        else
        {
            playerDisplayerInGame_player3.gameObject.SetActive(false);
        }

        if (GameManager.Instance.player4 != null)
        {
            playerDisplayerInGame_player4.gameObject.SetActive(true);
        }

        else
        {
            playerDisplayerInGame_player4.gameObject.SetActive(false);
        }
    }

    public void SetCountdown(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        if(minutes < 10)
        {
            countdown.text = "0" + minutes.ToString();
        }

        else
        {
            countdown.text = minutes.ToString();
        }

        countdown.text += ":";

        if (seconds < 10)
        {
            countdown.text = "0" + seconds.ToString();
        }

        else
        {
            countdown.text = seconds.ToString();
        }

        if(time < 10)
        {
            countdown.color = countdownColor_LastSeconds;
        }

        else
        {
            countdown.color = countdownColor_Normal;
        }
    }
}
