using UnityEngine;
using UnityEngine.SceneManagement;


public class Game_UI : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.PlayGame();
    }

    public void Resume()
    {
        GameManager.instance.Resume();
    }
 

    public void Reset()
    {
        GameManager.instance.LoadGame();
    }


    public void QuitGame()
    {
        Application.Quit();
    } 
}
