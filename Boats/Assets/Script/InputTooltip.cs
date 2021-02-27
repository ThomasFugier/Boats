using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTooltip : MonoBehaviour
{
    public enum TooltipType {MainButton, BackButton};
    
    public UnityEngine.UI.Image filler;

    public void OnEnable()
    {
        
    }

    public void Fill(float fillAmount)
    {
        filler.fillAmount = fillAmount;
    }
}
