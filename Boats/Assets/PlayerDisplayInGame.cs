using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplayInGame : MonoBehaviour
{
    public PlayerManager linkedPlayer;
    private bool isLinkedToPlayer = false;
    public UnityEngine.UI.Image littleFlag;
    public UnityEngine.UI.Text littleFlagText;
    public UnityEngine.UI.Text coins;

    void Start()
    {
        
    }

    void Update()
    {
        if(isLinkedToPlayer == false)
        {
            if(linkedPlayer)
            {
                SetupColors();
                isLinkedToPlayer = true;

                
            }
        }

        if(linkedPlayer)
        {
            if(linkedPlayer.money < 10)
            {
                coins.text = "0" + linkedPlayer.money.ToString();
            }

            else
            {
                coins.text = linkedPlayer.money.ToString();
            }
           
        }
    }

    public void SetupColors()
    {
        Color c = Color.white;

        switch(linkedPlayer.playerIndex)
        {
            case PlayerIndex.Player1:
                c = GameManager.Instance.colorDefinitions.color_Player1;
                littleFlagText.text = "P1";
                break;

            case PlayerIndex.Player2:
                c = GameManager.Instance.colorDefinitions.color_Player2;
                littleFlagText.text = "P2";
                break;

            case PlayerIndex.Player3:
                c = GameManager.Instance.colorDefinitions.color_Player3;
                littleFlagText.text = "P3";
                break;

            case PlayerIndex.Player4:
                c = GameManager.Instance.colorDefinitions.color_Player4;
                littleFlagText.text = "P4";
                break;

            case PlayerIndex.AI:
                c = GameManager.Instance.colorDefinitions.color_AI;
                littleFlagText.text = "AI";
                break;
        }

        littleFlag.color = c;
        coins.color = c;
    }
}
