using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public enum MenuUsageType {Keyboard, Controller};

    public MenuUsageType usage;

    [Header("References")]
    public GameObject panel_mainMenu;
    public GameObject panel_playerSelection;
    public GameObject panel_InGame;

    [Header("First Selected")]
    public GameObject firstSelected_MainMenu;
    public GameObject firstSelected_playerSelection;

    public void Update()
    {
        if(InputManager.Instance.GetJoystickAxis_Y(PlayerIndex.Player1) != 0 || InputManager.Instance.GetJoystickAxis_X(PlayerIndex.Player1) != 0)
        {
            if(usage == MenuUsageType.Keyboard)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstSelected_MainMenu);
            }
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            usage = MenuUsageType.Keyboard;
        }

        else
        {
            usage = MenuUsageType.Controller;
        }

        if(usage == MenuUsageType.Controller)
        {
            this.GetComponent<UnityEngine.UI.GraphicRaycaster>().enabled = false;
        }

        else
        {
            this.GetComponent<UnityEngine.UI.GraphicRaycaster>().enabled = true;
        }
    }

    public void backFromPlayerSelection()
    {
        SetPanelState(true, panel_mainMenu);
        SetPanelState(false, panel_playerSelection);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected_MainMenu);
    }

    public void GoToPlayerSelection()
    {
        SetPanelState(false, panel_mainMenu);
        SetPanelState(true, panel_playerSelection);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected_playerSelection);
    }

    public void GoToGameplay()
    {
        SetPanelState(false, panel_playerSelection);
        SetPanelState(false, panel_mainMenu);
        SetPanelState(true, panel_InGame);
    }

    public void SetPanelState(bool state, GameObject panel)
    {
        panel.SetActive(state);
    }

    private void OnEnable()
    {
        EnableMainMenu();
    }

    public void EnableMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected_MainMenu);
    }

    public void OnMouseOver()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
