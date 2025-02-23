using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instans;

    [SerializeField] private GameObject gameUI;

    [SerializeField] public GameObject mainMenu;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI pointText;

    private float currentponit;
         
    private void Awake()
    {
        if(instans == null)
        {
            instans = this;
        }    
        else
            Destroy(instans);
    }


    private void Start()
    {
        pointText.text = currentponit.ToString();

        AudioManager.instance.StopAUDIO();
        MainMenu();
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }


    public void Addpoint()
    {
        currentponit++;
        pointText.text = currentponit.ToString();
    }    

    #region UI

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        pause.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 0;
    }


    public void PauseGame()
    {
        mainMenu.SetActive(false);
        pause.SetActive(true);
        gameOver.SetActive(false);
        Time.timeScale = 0;
    }


    public void GameOver()
    {
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(true);
        AudioManager.instance.StopAUDIO();
        Time.timeScale = 0;
    }


    public void PlayGame()
    {
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1;
    }


    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1;
    }


    #endregion


}
