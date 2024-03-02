using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Box : MonoBehaviour
{
    public GameObject UIManagerObj;
    private UIManager _UIManager;

    //public GameObject BoxUI;

    private void Awake()
    {
        _UIManager = UIManagerObj.GetComponent<UIManager>();
    }

    //public void OnInteraction()
    //{
    //    if (Trigger.inShop == true)
    //        ShopOpen();
    //}

    //public void ShopOpen()
    //{
    //    Debug.Log("ShopOpen");
    //    //_UIManager.UIOn(BoxUI);
    //}
}
