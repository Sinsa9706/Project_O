using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Light2D globalLight; // Light2D 컴포넌트 참조
    public Color dayColor = Color.white; // 낮 색
    public Color nightColor = new Color(30 / 255f, 130 / 255f, 255 / 255f); // 밤 색
    public float transitionDuration = 10.0f; // 낮,밤 전환 시간

    public static int PlayerGold = 500;

    [Header("Time")]
    public TMP_Text TimeText;
    //public GameObject DarkImage;

    [Header("Player")]
    public TMP_Text PlayerGoldText;

    private float realTime = 1;//실제시간몇초당 10분
    private float time;
    private int gameTime = 360;

    public static bool isNight = false; //자는버튼

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
            //AlphaReset();
            TimeCheck();
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
        UpdateLightingTransition();
    }

    public void TimeCheck()
    {
        time += Time.deltaTime;

        if (time >= realTime)
        {
            gameTime += 10;
            time = 0;

            //AlphaChange();
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

    //public void AlphaChange()
    //{   
    //    Color color = new Color(0, 0, 0);
    //    color.a = 0.05f;

    //    if (gameTime <= 1140)
    //        return;

    //    if (DarkImage.GetComponent<SpriteRenderer>().color.a >= 0.78)
    //        return;

    //    DarkImage.GetComponent<SpriteRenderer>().color += color;
    //}
    //public void AlphaReset()
    //{
    //    Color color = new Color(0, 0, 0);

    //    color.r = DarkImage.GetComponent<SpriteRenderer>().color.r;
    //    color.g = DarkImage.GetComponent<SpriteRenderer>().color.g;
    //    color.b = DarkImage.GetComponent<SpriteRenderer>().color.b;
    //    color.a = 0;

    //    DarkImage.GetComponent<SpriteRenderer>().color = color;
    //}

    public bool IsMorning() // 오전 7시부터 아침, 아침 되면 몬스터 리스폰
    {
        return gameTime >= 420 && gameTime < 430;
    }

    public bool IsNight() // 오후 6시부터 다음날 오전 6시까지 밤으로 판단
    {
        return gameTime >= 1080 || gameTime < 360; // 1080은 오후 6시, 360은 오전 6시를 의미
    }

    private void UpdateLightingTransition()
    {
        float currentHour = gameTime / 60.0f; // 현재 시간을 시간 단위로 변환
        float lerpFactor;

        // 오전 6시부터 낮 색상으로 전환 시작
        if (currentHour >= 6f && currentHour < 7f) // 오전 6시부터 1시간 동안
        {
            lerpFactor = (currentHour - 6f) / 1f; // 오전 6시부터 1시간 동안 보간 계산
            globalLight.color = Color.Lerp(nightColor, dayColor, lerpFactor);
        }
        else if (currentHour >= 17f && currentHour < 18f) // 오후 5시부터 1시간 동안 밤으로 전환 시작
        {
            lerpFactor = (currentHour - 17f) / 1f; // 오후 5시부터 1시간 동안 보간
            globalLight.color = Color.Lerp(dayColor, nightColor, lerpFactor);
        }
        else if (currentHour >= 7f && currentHour < 17f) // 오전 7시부터 오후 5시까지는 낮 색상 유지
        {
            globalLight.color = dayColor;
        }
        else // 그 외 시간에는 밤 색상 유지
        {
            globalLight.color = nightColor;
        }
    }
}
