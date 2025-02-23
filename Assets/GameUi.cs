using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.StartGame();
       
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    public void ContinueGame()
    {
        GameManager.instance.ResumeGame();
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
