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
    public GameObject BoxInteractionUI;
    public GameObject QuestInteractionUI;

    public static bool inShop = false;
    public static bool inBox = false;
    public static bool inQuest = false;

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
        if (collision.tag == "Box")
        {
            _UIManager.UIOn(BoxInteractionUI);
            inBox = true;
        }
        if (collision.tag == "Quest")
        {
            _UIManager.UIOn(QuestInteractionUI);
            inQuest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shop")
        {
            _UIManager.UIOff(InteractionUI);
            inShop = false;
        }
        if (collision.tag == "Box")
        {
            _UIManager.UIOff(BoxInteractionUI);
            inBox = false;
        }
        if (collision.tag == "Quest")
        {
            _UIManager.UIOff(QuestInteractionUI);
            inQuest = false;
        }
    }
}
