using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource audioSource;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySFX(string name,AudioClip clip)
    {
        if (!GameManager.Instance.isSfxMute)
        {
            GameObject obj = new GameObject(name + "SOUND");
            AudioSource AS = obj.AddComponent<AudioSource>();
            AS.clip = clip;
            AS.volume = GameManager.Instance.sfxVoloume;
            AS.Play();

            Destroy(obj, AS.clip.length);
        }
        
    }


    public void backgroundSoundPlay(AudioClip clip, bool isMute)
    {
        if (isMute)
        {
            audioSource.volume = 0;
        }
        else
        {
            if (!GameManager.Instance.isMusicMute)
            {
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.volume = GameManager.Instance.musicVoloume;
                audioSource.Play();
            }
            else
            {
                audioSource.volume = 0;

            }

        }
        
    }
}
