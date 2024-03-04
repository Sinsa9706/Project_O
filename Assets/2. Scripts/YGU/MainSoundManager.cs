using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static SoundManager instance;
    

    [SerializeField][Range(0f, 1f)] private float musicVolume;
    

    private AudioSource musicAudioSource;
    

    public List<AudioClip> musicClip = new();

    private void Awake()
    { 

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;


    }

    public void Soundplay(int getint)
    {
        musicAudioSource.clip = musicClip[getint];
        musicAudioSource.Play();
    }

    private void Start()
    { 
        Soundplay(0);
        
    }

}