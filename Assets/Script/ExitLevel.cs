using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextlevel());
    }

    IEnumerator LoadNextlevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        int manhientai = SceneManager.GetActiveScene().buildIndex; // lấy thông tin màn hiện tại
        int Quaman = manhientai + 1;

        if(Quaman == SceneManager.sceneCountInBuildSettings)
        {
            Quaman = 0;
        }
        FindObjectOfType<Scenepersist>().ResetScenePersist();
        SceneManager.LoadScene(Quaman);

    }    
}
