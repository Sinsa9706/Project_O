using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static SoundManager instance;
    // SoundManager의 인스턴스를 다른 스크립트에서 쉽게 접근할 수 있도록 하는 정적 변수

    [SerializeField][Range(0f, 1f)] private float musicVolume;
    // 효과음 볼륨 및 피치 변동, 배경 음악 볼륨을 조절하기 위한 변수들

    private AudioSource musicAudioSource;
    // 배경 음악을 재생하기 위한 AudioSource 및 현재 재생 중인 배경 음악 클립

    public List<AudioClip> musicClip = new();

    private void Awake()
    { // 스크립트가 활성화될 때 호출되는 Awake 메서드

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
        // 필요한 컴포넌트들을 초기화

    }

    public void Soundplay(int getint)
    {
        musicAudioSource.clip = musicClip[getint];
        musicAudioSource.Play();
    }

    private void Start()
    { // 스크립트가 시작될 때 호출되는 Start 메서드
        Soundplay(0);
        //아침
    }

}