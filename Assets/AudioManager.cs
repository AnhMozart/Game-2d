using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource ads_Basic;
    [SerializeField] private AudioSource ads_BG;

    [SerializeField] private AudioClip Jump;
    [SerializeField] private AudioClip Die;
    [SerializeField] private AudioClip coin;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }


    public void PlayJumpSound()
    {
        ads_Basic.PlayOneShot(Jump);
    }


    public void PlayDieSound()
    {
        ads_Basic.PlayOneShot(Die);
    }


    public void PlayCoinSound()
    {
        ads_Basic.PlayOneShot(coin);
    }
    

    public void PlayBGSound()
    {
        ads_BG.Play();
    }

    public void StopAUDIO()
    {
        ads_BG.Stop();
        ads_Basic.Stop();
    }
}
