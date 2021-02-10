using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTooltip : MonoBehaviour
{
    public UnityEngine.UI.Image filler;

    public void Fill(float fillAmount)
    {
        filler.fillAmount = fillAmount;
    }
}
