using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [Header("References")]
    [Space]
    public Renderer[] renderersToColor;

    public void SetPlayerColor()
    {
        Color appliedColor = Color.black;

        switch(this.GetComponent<PlayerManager>().playerIndex)
        {
            case PlayerIndex.Player1:
                appliedColor = GameManager.Instance.colorDefinitions.color_Player1;
                break;

            case PlayerIndex.Player2:
                appliedColor = GameManager.Instance.colorDefinitions.color_Player2;
                break;

            case PlayerIndex.Player3:
                appliedColor = GameManager.Instance.colorDefinitions.color_Player3;
                break;

            case PlayerIndex.Player4:
                appliedColor = GameManager.Instance.colorDefinitions.color_Player4;
                break;

            case PlayerIndex.AI:
                appliedColor = GameManager.Instance.colorDefinitions.color_AI;
                break;

                
        }

        foreach (Renderer r in renderersToColor)
        {
            r.material.SetColor("_Color", appliedColor);
        }
    }
}
