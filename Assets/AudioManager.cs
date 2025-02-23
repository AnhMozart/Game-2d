using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource ads;
    [SerializeField] private AudioSource DefaulAds;
    [SerializeField] private AudioSource BossAds;

    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip energyClip;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    public void PlayShootSound()
    {
        ads.PlayOneShot(shootingClip);
    }

    public void PlayReloadSound()
    {
        ads.PlayOneShot(reloadClip);
    }

    public void PlayEnergySound()
    {
        ads.PlayOneShot(energyClip);
    }


    public void PlayDefaulSound()
    {
        DefaulAds.Play();
        BossAds.Stop();
    }

    public void PlayBossSound()
    {
        DefaulAds.Stop();
        BossAds.Play();
    }

    public void StopSound()
    {
        DefaulAds.Stop();
        BossAds.Stop();
        ads.Stop();
    }

}
