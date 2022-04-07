using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public AudioClip BtnClickClip;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public GameObject musicMute;
    public GameObject sfxMute;
    public void changeScene(string name)
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlaySFX("btnClick", BtnClickClip);
        GameManager.Instance.isGameControl = false;
        SoundManager.Instance.backgroundSoundPlay(null, true);
        SceneManager.LoadScene(name);
    }

    public void showIt(GameObject obj)
    {
        SoundManager.Instance.PlaySFX("btnClick", BtnClickClip);
        obj.SetActive(true);
    }
    public void hideIt(GameObject obj)
    {
        SoundManager.Instance.PlaySFX("btnClick", BtnClickClip);
        obj.SetActive(false);
    }


    public void gamePause()
    {
        GameManager.Instance.isGameControl = true;
        Time.timeScale = 0;
    }

    public void gameContinue()
    {
        Time.timeScale = 1;
        GameManager.Instance.isGameControl = false;

    }


    public void openSetting()
    {
        musicVolumeSlider.value = GameManager.Instance.musicVoloume;
        sfxVolumeSlider.value = GameManager.Instance.sfxVoloume;
    }

    public void changeMusicValue()
    {
        GameManager.Instance.musicVoloume = musicVolumeSlider.value;
        SoundManager.Instance.GetComponent<AudioSource>().volume = musicVolumeSlider.value;
    }

    public void changeSFXValue()
    {
        GameManager.Instance.sfxVoloume = sfxVolumeSlider.value;
    }

    public void muteSound(bool isMusic)
    {
        if (isMusic)
        {
            if(GameManager.Instance.isMusicMute)
                musicMute.SetActive(false);
            else
                musicMute.SetActive(true);

            GameManager.Instance.isMusicMute = !GameManager.Instance.isMusicMute;

            if (GameManager.Instance.isMusicMute)
            {
                SoundManager.Instance.GetComponent<AudioSource>().volume =0;
                GameManager.Instance.musicVoloume = 0;
                musicVolumeSlider.value = 0;

            }
        }
        else
        {
            if (GameManager.Instance.isSfxMute)
                sfxMute.SetActive(false);
            else
                sfxMute.SetActive(true);

            GameManager.Instance.isSfxMute = !GameManager.Instance.isSfxMute;

            if (GameManager.Instance.isSfxMute)
            {
                GameManager.Instance.sfxVoloume = 0;
                sfxVolumeSlider.value = 0;
            }
        }
    }

}
