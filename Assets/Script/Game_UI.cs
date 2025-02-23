using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game_UI : MonoBehaviour
{
    public void PlayeGame()
    {
        GameManager.instans.PlayGame();
        AudioManager.instance.PlayBGSound();
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.StopAUDIO();
    }


    public void Resume()
    {
        GameManager.instans.ResumeGame();
    }

    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
