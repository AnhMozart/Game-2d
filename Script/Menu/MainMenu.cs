using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;


public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip Button;
    private AudioSource Ads;


    private void Start()
    {
        Ads = GetComponent<AudioSource>();
    }



    public void PlayGame()
    {
        Ads.PlayOneShot(Button);
        StartCoroutine(LoadLevelAfterDelay(1f)); // Start a coroutine with a 2-second delay
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadSceneAsync(1);         // Load the scene after the delay
    }


    public void PlayLevel(int level)
    {
        Ads.PlayOneShot(Button, level);
        StartCoroutine(PlaylevelDelay(level,1f));
    }

    IEnumerator PlaylevelDelay(int level, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(level);

    }

    public void Quit()
    {
        Application.Quit();
        Ads.PlayOneShot(Button);
    }
}
