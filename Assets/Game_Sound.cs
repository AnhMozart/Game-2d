using UnityEngine;

public class Game_Sound : MonoBehaviour
{
    public static Game_Sound instans;

    [SerializeField] private AudioSource ads_Basic;
    [SerializeField] private AudioSource ads_BG;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip scoreSound;

    private void Awake()
    {
        if(instans == null)
        {
            instans = this;
        }    

        else
            Destroy(gameObject);
    }


    public void PlayJumpSound()
    {
        ads_Basic.PlayOneShot(jumpSound);
    }


    public void PlayDieSound()
    {
        ads_Basic?.PlayOneShot(dieSound);  
    }


    public void PlayScoreSound()
    {
        ads_Basic.PlayOneShot(scoreSound);
    } 

    
    public void PlayBGSound()
    {
        ads_BG.Play();
    }    


    public void StopBGSound()
    {
        ads_BG.Stop();
    }    

    public void StopAudio()
    {
        ads_Basic.Stop();
        ads_BG?.Stop();
    }    
}
