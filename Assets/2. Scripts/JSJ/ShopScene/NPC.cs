using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject UIManagerObj;
    private UIManager _UIManager;

    public GameObject ShopUI = null;
    public GameObject BoxUI = null;

    private void Awake()
    {
        _UIManager = UIManagerObj.GetComponent<UIManager>();
    }

    public void OnInteraction()
    {
        if(Trigger.inShop == true)
            ShopOpen();
        if (Trigger.inBox == true)
            BoxOpen();
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
}
