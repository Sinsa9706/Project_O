using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager instance;

    [SerializeField][Range(0f, 1f)] private float musicVolume = 0.2f; // BGM ����
    [SerializeField][Range(0f, 1f)] private float sfxVolume = 0.5f; // SFX (ȿ����) ����

    private AudioSource musicAudioSource; // ������ǿ� AudioSource
    private AudioSource sfxAudioSource; // ȿ������ AudioSource

    public List<AudioClip> musicClips; // BGM Ŭ�� ����Ʈ
    public List<AudioClip> sfxClips; // SFX Ŭ�� ����Ʈ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // �ʿ��� ������Ʈ�� �ʱ�ȭ

        // BGM�� AudioSource ����
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;

        // SFX�� AudioSource ���� ( loop ���� )
        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource.volume = sfxVolume; // SFX ���� ����

    }

    // �� ������ ����ǰ� �ִ���
    public bool IsPlayingDayMusic()
    {
        return musicAudioSource.clip == musicClips[0] && musicAudioSource.isPlaying;
    }

    // �� ������ ����ǰ� �ִ���
    public bool IsPlayingNightMusic()
    {
        return musicAudioSource.clip == musicClips[1] && musicAudioSource.isPlaying;
    }

    public void PlayMusic(int index)
    {
        musicAudioSource.clip = musicClips[index];
        musicAudioSource.Play();
    }

    public void PlaySFX(int index)
    {
        sfxAudioSource.PlayOneShot(sfxClips[index], sfxVolume);
    }

    private void Start()
    {
        PlayMusic(0); // �� BGM ���
    }

}