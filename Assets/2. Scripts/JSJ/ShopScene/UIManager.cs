using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static int Count = 0;
    
    //ON OFF Change
    public void UIOnOff(GameObject ui)
    {
        if (ui.activeSelf == true)
            ui.SetActive(false);
        else
            ui.SetActive(true);
    }

    //UION / OFF
    public void UIOn(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void UIOff(GameObject ui)
    {
        ui.SetActive(false);
        Time.timeScale = 1.0f;
    }

    //CountController
    public void CountPlus(TMP_Text text)
    {
        Count = int.Parse(text.text);
        Count++;
        text.text = Count.ToString();
        PriceText();
    }
    public void CountMinus(TMP_Text text)
    {
        Count = int.Parse(text.text);
        if (Count <= 0)
            return;
        Count--;
        text.text = Count.ToString();
        PriceText();//Text연결하기

    }
    public void CountReset(TMP_Text text)
    {
        Count = 0;
        text.text = Count.ToString();
    }

    public void PriceText()
    {
        int price = 50 * Count;//50은 인벤
    }



}
