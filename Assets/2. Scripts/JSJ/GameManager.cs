using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int PlayerGold = 500;

    public TMP_Text TimeText;
    public GameObject Image;

    private float realTime = 1;//실제시간몇초당 10분
    private float time;
    private int gameTime = 360;

    public static bool isNight = false;//자는버튼

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
        if (isNight == true)
        {
            time = 0;
            gameTime = 360;
            isNight = false;
            AlphaReset();
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
            TimeTextChange();
        }
    }

    public void TimeTextChange(bool isDay = false)
    {
        string text;

        if (gameTime >= 1440)
            text = "오전  ";
        else if (gameTime >= 720)
            text = "오후  ";
        else
            text = "오전  ";

        string hour;
        hour = (gameTime / 60).ToString("D2");

        if (gameTime / 60 >= 13)
            hour = ((gameTime / 60) % 12).ToString("D2");

        string minute;
        minute = (gameTime % 60).ToString("D2");

        TimeText.text = text + hour + " : " + minute;

        if (isDay == true)
            TimeText.text = "오전  06 : 00";
    }

    public void AlphaChange()
    {   
        Color color = new Color(0, 0, 0);
        color.a = 0.05f;

        if (gameTime <= 1140)
            return;

        if (Image.GetComponent<SpriteRenderer>().color.a >= 0.78)
            return;

        Image.GetComponent<SpriteRenderer>().color += color;
    }
    public void AlphaReset()
    {
        Color color = new Color(0, 0, 0);

        color.r = Image.GetComponent<SpriteRenderer>().color.r;
        color.g = Image.GetComponent<SpriteRenderer>().color.g;
        color.b = Image.GetComponent<SpriteRenderer>().color.b;
        color.a = 0;

        Image.GetComponent<SpriteRenderer>().color = color;
    }

    //0~255 /200 25.5 = 0.1
}
