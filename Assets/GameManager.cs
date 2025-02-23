using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private float score = 0f;
    [SerializeField] private float lives = 3;
    [SerializeField] private Text High_Score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private float gameSpeed = 5f;
    [SerializeField] private float speedIncrease = 0.15f;

    private const string high_Score_Key = "HighScore"; // Khóa để lưu điểm cao nhất

    [Space]
    [Header("Game_UI")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 

        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // Giữ GameManager khi chuyển cảnh

    }


    private void Start()
    {
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
        MainMenu();
        Game_Sound.instans.StopAudio();
        LoadHighScore();
    }

    private void Update()
    {
        UpdateGameSpeed();

        Debug.Log(gameSpeed);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }    
    }


    public float GetGameSpeed()
    {
        return gameSpeed;
    }


    private void UpdateGameSpeed()
    {
        gameSpeed += Time.deltaTime * speedIncrease;
    }    


    public void AddCoin(float _coin)
    {
        score += _coin;
        scoreText.text = score.ToString();
        Game_Sound.instans.PlayScoreSound();
        CheckAndSaveHighScore();
    }    
    

    public void UppdatetoLive()
    {
        lives--;
        livesText.text = lives.ToString();
        if(lives <= 0)
        {
            Debug.Log("Bạn đã chết");
            Game_Sound.instans.PlayDieSound();
            CheckAndSaveHighScore();
            GameOver();
        }    
    }


    #region High_Score
    private float GetHighScore()
    {
        return PlayerPrefs.GetFloat(high_Score_Key, 0f); //Gía trị mạc định là 0;
    }    

    private void CheckAndSaveHighScore()
    {
        if(score > GetHighScore())
        {
            SaveHighScore(score);
            UpdateHighScoreText();
        }    
            
    }

    private void SaveHighScore(float highScore)
    {
        PlayerPrefs.SetFloat(high_Score_Key, highScore);
        PlayerPrefs.Save();
    }    



    private void UpdateHighScoreText()
    {
        if(High_Score != null)
        {
            High_Score.text = "High Score: " + GetHighScore().ToString();
        }    
    }


    private void LoadHighScore()
    {
        UpdateHighScoreText(); // Hiển thị điểm cao nhất khi bắt đầu game
    }
    #endregion


    #region Game_UI
    private void MainMenu()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 0f;
    }


    private void PauseMenu()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
        gameOver.SetActive(false);
        Time.timeScale = 0f;
    }


    private void GameOver()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(true);
        Game_Sound.instans.StopBGSound();
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        Game_Sound.instans.PlayBGSound();
        Time.timeScale = 1f;
    }


    public void Resume()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadGame()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        LoadScene();
    }    

    #endregion
    private void LoadScene()
    {
        lives = 3;
        livesText.text = lives.ToString();

        score = 0;
        scoreText.text = score.ToString();

        gameSpeed = 5f;

        Game_Sound.instans.PlayBGSound();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
