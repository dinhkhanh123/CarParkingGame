using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Element")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Sound sound;
    [SerializeField] private Sprite optionOnMusic;
    [SerializeField] private Sprite optionOffMusic;
    [SerializeField] private Sprite optionOnSound;
    [SerializeField] private Sprite optionOffSound;
    [SerializeField] private Image mucsicButtonImage;
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Header(" Setting ")]
    private bool musicState = true;
    private bool soundState = true;

    private void Awake()
    {
        musicState = PlayerPrefs.GetInt("music", 1) == 1;
        soundState = PlayerPrefs.GetInt("sound", 1) == 1;

    }

    private void Start()
    {
        if(instance == null)
            instance = this;


        Setup();
    }

    private void Update()
    {
        if (musicSlider.value > 0)
        {
            mucsicButtonImage.sprite = optionOnMusic;

        }
        else
        {
            mucsicButtonImage.sprite = optionOffMusic;
        }

        if (soundSlider.value > 0)
        {
            soundButtonImage.sprite = optionOnSound;
        }
        else
        {

            soundButtonImage.sprite = optionOffSound;
        }
    }

    private void Setup()
    {
        if (musicState)
            EnableMusic();
        else
            DisableMusic();

        if (soundState)
            EnableSound();
        else
            DisableSound();
    }

    public void ChangeMusicState()
    {
        if (musicState)
            DisableMusic();
        else
            EnableMusic();

        musicState = !musicState;

        PlayerPrefs.SetInt("music", musicState ? 1 : 0);
    }

    public void ChangeSoundState()
    {
        if (soundState)
            DisableSound();
        else
            EnableSound();

        soundState = !soundState;

        PlayerPrefs.SetInt("sound", soundState ? 1 : 0);
    }

    public void EnableMusic()
    {
        sound.EnableMusic();
        musicSlider.value = 1;
        mucsicButtonImage.sprite = optionOnMusic;
    }

    public void DisableMusic()
    {
        sound.DisableMusic();
        musicSlider.value = 0;  
        mucsicButtonImage.sprite = optionOffMusic;
    }

    private void EnableSound()
    {

        sound.EnableSound();
        soundSlider.value = 1;
        soundButtonImage.sprite = optionOnSound;
    }

    private void DisableSound()
    {
        sound.DisableSound();
        soundSlider.value = 0;  
        soundButtonImage.sprite = optionOffSound;
    }

    public void MusicVolume()
    {
        Sound.instance.MusicVolume(musicSlider.value);
    }

    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }
    public void HideSettingPanel()
    {
        settingPanel.SetActive(false);
    }
}
