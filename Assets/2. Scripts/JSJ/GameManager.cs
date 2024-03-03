using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text TimeText;
    public GameObject Image;

    [HideInInspector] public int Gold = 500;

    private float time;
    private float realTime = 1;//실제시간몇초당 10분
    private int gameTime = 360;

    private bool isSleep = false;//자는버튼

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (isSleep == true)
        {
            time = 0;
            gameTime = 360;
            isSleep = false;
            TimeCheck();
        }
        else
            TimeCheck();


    }

    public void TimeCheck()
    {
        time += Time.deltaTime;

        if (time >= realTime)
        {
            gameTime += 10;
            time = 0;
            AlphaChange();



            if (gameTime >= 1440)
                gameTime = 0;

            TimeTextChange();
        }


    }

    public void TimeTextChange()
    {
        string text;

        if (gameTime >= 1440)
            text = "오전 ";
        else if (gameTime >= 720)
            text = "오후 ";
        else
            text = "오전 ";

        TimeText.text = text + (gameTime / 60).ToString("D2") + " : " + (gameTime % 60).ToString("D2");
    }

    public void AlphaChange()
    {
        if (Image.GetComponent<SpriteRenderer>().color.a >= 0.78)
            return;

        Color color = new Color(0, 0, 0);
        color.a = 0.05f;

        Image.GetComponent<SpriteRenderer>().color += color;
    }


    //0~255 /200 25.5 = 0.1
}
