using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LongPressCanvasInput : MonoBehaviour
{
    public enum ButtonType {MainButton, SecondaryButton};

    public PlayerIndex playerIndex;
    public ButtonType button;
    public float speedPress = 2;
    public UnityEngine.UI.Image filler;

    private float timer;

    public UnityEvent OnButtonFinishedToBePressed;

    public bool buttonLocked = false;

    public void Update()
    {
        timer = Mathf.Clamp(timer, 0, 1);

        filler.fillAmount = timer;

        if(button == ButtonType.MainButton)
        {
            if(InputManager.Instance.GetJoystickButton_Main(playerIndex))
            {
                ButtonPressed();
            }

            else
            {
                ButtonNotPressed();
            }
        }

        else if (button == ButtonType.SecondaryButton)
        {
            if (InputManager.Instance.GetJoystickButton_Secondary(playerIndex))
            {
                ButtonPressed();
            }

            else
            {
                ButtonNotPressed();
            }
        }
    }

    public void ButtonPressed()
    {
        if(buttonLocked == false)
        {
            timer += Time.deltaTime * speedPress;

            if (timer >= 1)
            {
                OnButtonFinishedToBePressed.Invoke();
                buttonLocked = true;
            }
        }
        
    }

    public void ButtonNotPressed()
    {
         buttonLocked = false;
         timer = Mathf.Lerp(timer, 0, Time.deltaTime * 3);
    }
}
