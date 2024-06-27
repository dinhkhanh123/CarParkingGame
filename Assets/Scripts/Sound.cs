using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    public static Sound instance;

    [Header("Sound")]
    [SerializeField] private AudioSource musicSource;
    

    private void Start()
    {
        if(instance==null)
            instance = this;
    }

    public void EnableMusic()
    {
        musicSource.volume = 1;
    }

    public void DisableMusic()
    {
        musicSource.volume = 0;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void EnableSound()
    {
       // soundSource.volume = 1;
    }

    public void DisableSound()
    {
       // soundSource.volume = 0;
    }
}
