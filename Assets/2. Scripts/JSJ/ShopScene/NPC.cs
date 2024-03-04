using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("UI Manager")]
    public GameObject UIManagerObj;
    private UIManager _UIManager;

    [Header("NPC UI Object")]
    public GameObject ShopUI;
    public GameObject BoxUI;
    public GameObject QuestUI;
    public GameObject SleepUI;
    public GameObject NightUI;

    [Header("Shop UI Object")]
    public TMP_Text PlayerGoldText;
    public GameObject NoGoldUI;

    [Header("Box UI Object")]
    public List<TMP_Text> ItemCount;

    private bool isDarkCoroutineEnd = false;
    private bool isDayStart = false;
    private bool isLightCoroutineEnd = false;

    private void Awake()
    {
        _UIManager = UIManagerObj.GetComponent<UIManager>();        
    }

    private void Start()
    {
        PlayerGoldUpdate();
    }

    private void Update()
    {
        if (isDarkCoroutineEnd == true)
        { 
            StopCoroutine("DarkUI");
            isDarkCoroutineEnd = false;
            Time.timeScale = 1;
            Invoke("NightSetting", 2);
        }
        if(isDayStart == true)
        {
            StartCoroutine("LightUI");
            Time.timeScale = 0;
            GameManager.isNight = true;
            GameManager.Instance.TimeTextChange(true);
            isDayStart = false;
        }
        if (isLightCoroutineEnd == true)
        {
            StopCoroutine("LightUI");
            isLightCoroutineEnd = false;
            Time.timeScale = 1;
            _UIManager.UIOff(SleepUI);
        }
    }

    public void OnInteraction()//Trigger가 활성화 된 상태로 z를 눌렀을때
    {
        //in시리즈 = 충돌된 태그체크와 들어와있는지 체크
        if (Trigger.inShop == true)
            ShopOpen();
        if (Trigger.inBox == true)
            BoxOpen();
        if (Trigger.inQuest == true)
            QuestOpen();
        if (Trigger.isSleep == true)
            SleepOpen();
    }

    private void ShopOpen()
    {
        Debug.Log("ShopOpen");
        _UIManager.UIOn(ShopUI);
        Time.timeScale = 0.0f;
    }

    private void BoxOpen()
    {
        Debug.Log("BoxOpen");
        _UIManager.UIOn(BoxUI);
        Time.timeScale = 0.0f;
    }

    private void QuestOpen()
    {
        Debug.Log("QuestOpen");
        _UIManager.UIOn(QuestUI);
        Time.timeScale = 0.0f;
    }

    IEnumerator DarkUI()
    {
        while (!(SleepUI.GetComponent<Image>().color.a >= 1))
        {
            isDarkCoroutineEnd = false;

            Color color = SleepUI.GetComponent<Image>().color;
            color.a = color.a + 0.001f;
            SleepUI.GetComponent<Image>().color = color;

            if (SleepUI.GetComponent<Image>().color.a >= 1)
            {
                isDarkCoroutineEnd = true;
            }

            yield return null;
        }
    }

    IEnumerator LightUI()
    {
        while (!(SleepUI.GetComponent<Image>().color.a <= 0))
        {
            isLightCoroutineEnd = false;

            Color color = SleepUI.GetComponent<Image>().color;
            color.a = color.a - 0.002f;
            SleepUI.GetComponent<Image>().color = color;

            if (SleepUI.GetComponent<Image>().color.a <= 0)
            {
                isLightCoroutineEnd = true;
            }

            yield return null;
        } 
    }

    private void SleepOpen()
    {
        Debug.Log("SleepOpen");
        _UIManager.UIOn(SleepUI);
        Time.timeScale = 0.0f;

        StartCoroutine("DarkUI");
    }

    private void NightSetting()
    {
        ItemSellGold();

        foreach (var gold in ItemCount)
        {
            gold.text = 0.ToString();
        }

        isDayStart = true;
    }

    public void ItemSellGold()
    {
        int sellGold = 0;

        //foreach(var gold in ItemCount)
        //{
        //    sellGold += int.Parse(gold.text)*50;//50은 
        //}



        sellGold += int.Parse(ItemCount[0].text) * 50;
        sellGold += int.Parse(ItemCount[1].text) * 50;
        sellGold += int.Parse(ItemCount[2].text) * 50;
        sellGold += int.Parse(ItemCount[3].text) * 50;
        sellGold += int.Parse(ItemCount[4].text) * 50;
        sellGold += int.Parse(ItemCount[5].text) * 50;

        GameManager.PlayerGold += sellGold;
        PlayerGoldUpdate();
    }

    public void PriceText(TMP_Text priceText)
    {
        int price = Shop.ItemGold * UIManager.Count;//50은 인벤
        priceText.text = price.ToString();
    }

    public void PlayerGoldChange(TMP_Text priceText)
    {
        if (GameManager.PlayerGold < int.Parse(priceText.text))
        {
            NoGoldUI.SetActive(true);
            return;
        }

        GameManager.PlayerGold -= int.Parse(priceText.text);
    }

    public void PlayerGoldUpdate()
    {
        PlayerGoldText.text = GameManager.PlayerGold.ToString();
        GameManager.Instance.PlayerGoldText.text = GameManager.PlayerGold.ToString();
    }
}
