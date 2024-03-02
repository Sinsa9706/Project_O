using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("UI Manager")]
    public GameObject UIManagerObj;
    private UIManager _UIManager;

    [Header("NPC UI Object")]
    public GameObject ShopUI;
    public GameObject BoxUI;
    public GameObject QuestUI;

    private void Awake()
    {
        _UIManager = UIManagerObj.GetComponent<UIManager>();
    }

    public void OnInteraction()//Trigger가 활성화 된 상태로 z를 눌렀을때
    {
        //in시리즈 = 충돌된 태그체크와 들어와있는지 체크
        if(Trigger.inShop == true)
            ShopOpen();
        if (Trigger.inBox == true)
            BoxOpen();
        if (Trigger.inQuest == true)
            QuestOpen();
    }

    public void ShopOpen()
    {
        Debug.Log("ShopOpen");
        _UIManager.UIOn(ShopUI);
        Time.timeScale = 0.0f;
    }

    public void BoxOpen()
    {
        Debug.Log("BoxOpen");
        _UIManager.UIOn(BoxUI);
        Time.timeScale = 0.0f;
    }

    public void QuestOpen()
    {
        Debug.Log("QuestOpen");
        _UIManager.UIOn(QuestUI);
        Time.timeScale = 0.0f;
    }


}
