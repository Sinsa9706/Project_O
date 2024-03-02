using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject BGMSoundObj;
    private AudioSource BGMSource;

    public GameObject EffectSoundObj;
    private AudioSource EffectSource;

    public List<AudioClip> AudioClipList;

    [HideInInspector] public bool effectSoundVolume = true;


    private void Awake()
    {
        BGMSource = BGMSoundObj.GetComponent<AudioSource>();
        EffectSource = EffectSoundObj.GetComponent<AudioSource>();
    }

    public void BGMSoundOnOff()
    {
        if (BGMSource.volume == 0)
            BGMSource.volume = 1;
        else
            BGMSource.volume = 0;
    }

    public void AudioClipPlay(int index)
    {
        EffectSource.PlayOneShot(AudioClipList[index], 1);
    }

    public void EffectSoundOnOff()
    {
        if (effectSoundVolume == true)
            effectSoundVolume = false;
        else
            effectSoundVolume = true;
    }

}
