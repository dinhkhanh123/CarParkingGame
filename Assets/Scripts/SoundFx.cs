using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFx : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource collideSource;
    [SerializeField] private AudioSource completeSource;

    private void Start()
    {
        GameManager.instance.OnCarCollision += PlayColideSound;
        GameManager.instance.OnCarEnterPark += PlayCompleteSound;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnCarCollision -= PlayColideSound;
    }
    private void PlayColideSound()
    {
        collideSource.Play();
    }

    private void PlayCompleteSound(Route route)
    {
        completeSource.Play();
    }
}
