using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private CrowdSystem crowdSystem;
    private bool levelCompleted = false; // Thêm biến kiểm tra


    void Start()
    {
        levelCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectionDoor();
    }


    private void DetectionDoor()
    {
        Collider[] Collider = Physics.OverlapSphere(transform.position, 1);
        for (int i = 0; i < Collider.Length; i++)
        {
            if (Collider[i].TryGetComponent(out Door door))
            {
                Debug.Log("Bạn đã chạm cửa");
                int bonesAmount = door.GetBonesAmount(transform.position.x);
                BonusType bonusType = door.GetBonusType(transform.position.x);

                door.Disable();

                crowdSystem.ApplyBonus(bonusType, bonesAmount);
            }
            else if (Collider[i].tag == "Finish" && !levelCompleted) // Kiểm tra levelCompleted
            {
                Debug.Log("Ban da qua man");
                ChunkManager.instance.FinishDisable();
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplate);
                //SceneManager.LoadScene(0);
                levelCompleted = true; // Đặt levelCompleted thành true
            }
        }
    }

}    
