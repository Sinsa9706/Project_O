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
            AlphaReset();
        }
        else
            TimeCheck();

        // 모든 MobSpawnManager에 대해 몬스터를 리스폰
        if (IsMorning())
        {
            foreach (var spawnManager in FindObjectsOfType<MobSpawnManager>())
            // MobSpawnManager 스크립트가 달려있는 모든 오브젝트를 찾아서 각각 개체에 반복문 수행  
            {
                spawnManager.SpawnMonsters();
            }
        }

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

    public void TimeTextChange()
    {
        string text;

        if (gameTime >= 1440)
            text = "오전 ";
        else if (gameTime >= 720)
            text = "오후 ";
        else
            text = "오전 ";

        string hour;
        hour = (gameTime / 60).ToString("D2");

        if (gameTime / 60 >= 13)
            hour = ((gameTime / 60) % 12).ToString("D2");

        string minute;
        minute = (gameTime % 60).ToString("D2");


        TimeText.text = text + hour + " : " + minute;
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
    public bool IsMorning() // 오전 7시부터 아침, 아침 되면 몬스터 리스폰
    {
        return gameTime >= 420 && gameTime < 430;
    }

}
