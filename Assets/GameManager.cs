using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float currentEnergy; //năng lượng hiện tại để gọi boss
    [SerializeField] private float energyRequired = 3; //Năng lượng hiện tại

    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject enemy_Spawner;

    [SerializeField] private Image Energy_UI;
    [SerializeField] private GameObject Game_UI;

    private bool isCallBoss = false;

    [Space]
    [Header("Game_Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject winner;

    [SerializeField] private CinemachineCamera followCamera;


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
    }

    private void Start()
    {
        currentEnergy = 0;
        Boss.SetActive(false);
        UpdateTextEnergy();
        AudioManager.instance.StopSound();
        joystick.SetActive(false);
        followCamera.Lens.OrthographicSize = 5f;
        MainMenu();
    }




    public void AddEnergy()
    {
        if (isCallBoss) return;

        currentEnergy++;
        UpdateTextEnergy();
        if (currentEnergy == energyRequired)
        {
            CallBoss();
        }
    }
    
    private void CallBoss()
    {
        isCallBoss = true;
        enemy_Spawner.SetActive(false);
        Boss.SetActive(true);
        Game_UI.SetActive(false);
        AudioManager.instance.PlayBossSound();
        followCamera.Lens.OrthographicSize = 10f;
    }

    private void UpdateTextEnergy()
    {
        if(Game_UI != null)
        {
            float fillAmound = Mathf.Clamp01(currentEnergy / energyRequired); // hàm clamp01 sẽ trả về giá trị trong khoảng từ 0 - 1 nếu lớn hơn 1 trả về 1 nhỏ hơn =  0 thì về 0 còn trong khoảng trả về đúng giá trị 
            Energy_UI.fillAmount = fillAmound;
        } 
            
    }



#region GAMEMENU
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        winner.SetActive(false);
        Time.timeScale = 0f;
    }


    public void GameOver()
    {
        gameOver.SetActive(true); 
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        joystick.SetActive(false);
        winner.SetActive(false);
        enemy_Spawner.SetActive(false);
    }


    public void Winner()
    {
        winner.SetActive(true);
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        joystick.SetActive(false);
        enemy_Spawner.SetActive(false);
        Time.timeScale = 0;
    }


    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        gameOver.SetActive(false);
        mainMenu.SetActive(false);
        joystick.SetActive(false);
        winner.SetActive(false);
        Time.timeScale = 0;
    }


    public void StartGame()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        mainMenu.SetActive(false);
        winner.SetActive(false);
        joystick.SetActive(true);
        Time.timeScale = 1f;
        AudioManager.instance.PlayDefaulSound();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        mainMenu.SetActive(false);
        winner.SetActive(false);
        joystick.SetActive(true);
        Time.timeScale = 1f;
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }    
#endregion


}
