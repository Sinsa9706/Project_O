using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UIOnOff(GameObject ui)
    {
        if (ui.activeSelf == true)
            ui.SetActive(false);
        else
            ui.SetActive(true);
    }

    public void UIOn(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void UIOff(GameObject ui)
    {
        ui.SetActive(false);
    }
}
