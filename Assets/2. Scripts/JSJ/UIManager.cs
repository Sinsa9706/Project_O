using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text Info;
    public GameObject StartUI;

    private bool isMinColor = false;

    private void Update()
    {
        if (isMinColor == false)
            TextAnimation(-0.005f);
        else
            TextAnimation(0.005f);
        Debug.Log("돌아가는중");
    }

    private void TextAnimation(float val)
    {
        Color color = Info.color;
        color.a = color.a + val;
        Info.color = color;

        if (Info.color.a <= 0)
        {
            isMinColor = true;
            Debug.Log("true로 바뀜");
        }

        if (Info.color.a >= 1)
        {
            isMinColor = false;
            Debug.Log("false로 바뀜");

        }
    }

}
