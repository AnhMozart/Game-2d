using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlPanel;
    private bool isPaused = false;

    [SerializeField] AudioClip button;
    AudioSource Ads;


    void Start()
    {
        Ads = GetComponent<AudioSource>();
        pauseMenu.SetActive(false);
        controlPanel.SetActive(false);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (controlPanel.activeSelf)
            {
                controlPanel.SetActive(false);
                Debug.Log("Control panel hidden");
            }
            else
            {
                if (isPaused)
                {
                    Resume();
                    Debug.Log("Game is paused, resuming...");
                }
                else
                {
                    Pause();
                    Debug.Log("Game is running, pausing...");

                }
            }
        } 
    }
    public void Pause()
    {
        pauseMenu?.SetActive(true);
        Time.timeScale = 0f; //thoi gian game dung lai
        isPaused = true;
    }
    public void Home()
    {
        Ads.PlayOneShot(button);
        Time.timeScale = 1f;
        StartCoroutine(HomeDelay(0.5f));
    }


    IEnumerator HomeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Resume()
    {
        pauseMenu?.SetActive(false);
        Ads.PlayOneShot(button);
        Time.timeScale = 1f; //thoi gian game chay binh thuong
        isPaused = false;
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        Ads.PlayOneShot(button);
        StartCoroutine(RestartDelay(0.5f));

    }

    IEnumerator RestartDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Gamesession.islive = false;
        GetComponent<Gamesession>().addScore(100);

    }

    // trong khong 0 -1 la tua cham con tren 1 la tua nhanh
}
