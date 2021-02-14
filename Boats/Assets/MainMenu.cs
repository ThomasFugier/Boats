﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public enum MenuUsageType {Keyboard, Controller};

    public MenuUsageType usage;

    [Header("References")]
    public GameObject panel_mainMenu;
    public GameObject panel_playerSelection;

    [Header("First Selected")]
    public GameObject firstSelected_MainMenu;

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
    }

    public void backFromPlayerSelection()
    {
        SetPanelState(true, panel_mainMenu);
        SetPanelState(false, panel_playerSelection);
    }

    public void GoToPlayerSelection()
    {
        SetPanelState(false, panel_mainMenu);
        SetPanelState(true, panel_playerSelection);
    }

    public void GoToGameplay()
    {
        SetPanelState(false, panel_playerSelection);
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
}
