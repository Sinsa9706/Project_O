using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject UIManagerObj;
    private UIManager _UIManager;

    public GameObject ShopUI;

    private void Awake()
    {
        _UIManager = UIManagerObj.GetComponent<UIManager>();
    }

    public void OnInteraction()
    {
        if(Trigger.inShop == true)
            ShopOpen();
    }

    public void ShopOpen()
    {
        Debug.Log("ShopOpen");
        _UIManager.UIOn(ShopUI);
        Time.timeScale = 0.0f;
    }
}
