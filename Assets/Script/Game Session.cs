using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float LoadTime = 1f;

     void Awake()
    {
        // 1. Kiểm tra xem có bao nhiêu phiên bản GameSession đang tồn tại trong cảnh (scene).
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        // 2. Nếu đã có hơn 1 GameSession (khi quay lại một scene khác hoặc reload scene), thì phá hủy đối tượng hiện tại.
        if (numGameSession > 1)
        {
            Destroy(gameObject);// Hủy đối tượng hiện tại vì đã có GameSession khác tồn tại.
        }
        else
        {
            // 3. Nếu không có GameSession khác, đối tượng này sẽ được giữ lại khi tải màn chơi mới.
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = PlayerLives.ToString();
        scoreText.text = score.ToString();

    }

    void Update()
    {
    }  
    

    public void addScore(int addscore)
    {
        score += addscore;
        scoreText.text = score.ToString();

    }    

    public void ProcessPlayerDeath() //xử lý cái chết của người chơi
    {
        if (PlayerLives > 1)
        {
            PlayerLives--;
            StartCoroutine(Loadtime());
        }
        else 
        {
            ResetGameSession();
        }
    }

    IEnumerator Loadtime()
    {
        yield return new WaitForSeconds(LoadTime);
        int ManHienTai = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(ManHienTai);
        livesText.text = PlayerLives.ToString();
    }    

    void ResetGameSession()
    {
        FindObjectOfType<Scenepersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
