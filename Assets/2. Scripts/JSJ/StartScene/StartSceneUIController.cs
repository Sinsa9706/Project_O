using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartSceneUIController : MonoBehaviour
{
    public GameObject SoundManagerObj;

    public TMP_Text Info;
    public GameObject StartUI;
    public GameObject SelectUI;
    public GameObject SettingUI;

    private bool isMinColor = false;
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManagerObj.GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (isMinColor == false)
            TextAnimation(-0.005f);
        else
            TextAnimation(0.005f);

        if (Input.anyKeyDown)
        {
            if (soundManager.effectSoundVolume == true)
                soundManager.AudioClipPlay(0);

            if (SelectUI.activeSelf == true || Info.gameObject.activeSelf == true)
            {
                SelectUIOpen();
            }
        }
    }

    private void TextAnimation(float val)
    {
        Color color = Info.color;
        color.a = color.a + val;
        Info.color = color;

        if (Info.color.a <= 0)
            isMinColor = true;

        if (Info.color.a >= 1)
            isMinColor = false;
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void SelectUIOpen()
    {
        StartUI.SetActive(true);
        Info.gameObject.SetActive(false);
        SettingUI.SetActive(false);
        SelectUI.SetActive(true);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("2. MainField");
    }

    public void SettingUIOpen()
    {
        StartUI.SetActive(false);
        SelectUI.SetActive(false);
        SettingUI.SetActive(true);
    }

    public void SoundOnOffUI(GameObject icon)
    {
        if (icon.activeSelf == true)
            icon.SetActive(false);
        else
            icon.SetActive(true);
    }
}
