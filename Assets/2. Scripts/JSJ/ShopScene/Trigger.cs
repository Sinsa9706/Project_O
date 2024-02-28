using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Trigger : MonoBehaviour
{
    public GameObject UIManagerObj;
    private UIManager _UIManager;

    public GameObject InteractionUI;

    public static bool inShop = false;

    private void Awake()
    {
        _UIManager = UIManagerObj.GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Shop")
        {
            _UIManager.UIOn(InteractionUI);
            inShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shop")
        {
            _UIManager.UIOff(InteractionUI);
            inShop = false;
        }
    }
}
