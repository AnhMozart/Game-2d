using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject playGamePanel;
    [SerializeField] private Slider SpeedGamePlay;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject NextLevel;


    private void Start()
    {
        SpeedGamePlay.value = 0;
        playGamePanel.SetActive(false);
        NextLevel.SetActive(false);
        GameOver.SetActive(false);
        
        levelText.text = "Level " + ChunkManager.instance.GetLevel();
        GameManager.onGameStateChange += GameStateChangeCallBack;
    }


    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameStateChangeCallBack;
    }

    private void Update()
    {
        UpdateGameProgress();
    }


    private void GameStateChangeCallBack(GameManager.GameState _state)
    {
        if (_state == GameManager.GameState.GameOver)
        {
            Debug.Log("ĐÃ SHOW GAMEOVER");
            ShowGameOver();
        }    
        else if(_state == GameManager.GameState.LevelComplate)
        {
            ShowNextGame();
        }    

    }    


    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        playGamePanel.SetActive(true);
    }


    public void Resume()
    {
        SceneManager.LoadScene(0);
    }

    private void ShowNextGame()
    {
        playGamePanel.SetActive(false);
        GameOver.SetActive(false);
        NextLevel.SetActive(true);
    }    

    private void ShowGameOver()
    {
        playGamePanel.SetActive(false);
        NextLevel.SetActive(false);
        GameOver.SetActive(true);
    }    


    private void UpdateGameProgress()
    {
        if(!GameManager.instance.IsGameState())
            return;
        float Progress = PlayerController.instance.transform.position.z / ChunkManager.instance.ReturnPositionZFinish();
        SpeedGamePlay.value = Progress;
    }
}
