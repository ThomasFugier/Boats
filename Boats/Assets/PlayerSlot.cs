using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : MonoBehaviour
{
    public PlayerSelection playerSelection;

    public PlayerIndex playerIndex;

    public bool isPlayerReady;

    [Header("References")]
    public GameObject notSelectedImage;
    public GameObject selectedImage;
    public GameObject howToJoin;

    public GameObject tooltip_Cancel;
    public GameObject tooltip_Ready;
    public GameObject readyObject;

    public void OnEnable()
    { 
        PlayerDisconnect();
        tooltip_Cancel.GetComponent<LongPressCanvasInput>().playerIndex = playerIndex;
        tooltip_Ready.GetComponent<LongPressCanvasInput>().playerIndex = playerIndex;
    }

    public void PlayerConnect()
    {
        tooltip_Cancel.transform.parent.transform.parent.gameObject.SetActive(true);
        tooltip_Ready.transform.parent.transform.parent.gameObject.SetActive(true);
        notSelectedImage.SetActive(false);
        selectedImage.SetActive(true);
        howToJoin.SetActive(false);
    }

    public void PlayerDisconnect()
    {
        readyObject.SetActive(false);
        tooltip_Cancel.transform.parent.transform.parent.gameObject.SetActive(false);
        tooltip_Ready.transform.parent.transform.parent.gameObject.SetActive(false);
        notSelectedImage.SetActive(true);
        selectedImage.SetActive(false);
        howToJoin.SetActive(true);
    }

    public void PlayerReady()
    {
        isPlayerReady = true;
        readyObject.SetActive(true);
        tooltip_Ready.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    public void PlayerNoLongerReady()
    {
        isPlayerReady = false;
        readyObject.SetActive(false);
        tooltip_Ready.transform.parent.transform.parent.gameObject.SetActive(true);
    }

    public void RequestDisconnect()
    {
        if(isPlayerReady == false)
        {
            playerSelection.PlayerDisconnect(playerIndex);
        }
        
        else
        {
            playerSelection.PlayerNotReady(playerIndex);
        }
    }

    public void RequestPlayerReady()
    {
        playerSelection.PlayerReady(playerIndex);
    }
}
