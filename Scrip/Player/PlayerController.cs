using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Element")]
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Setting")]
    [SerializeField] private float moveSpeed = 5f;
    private bool canRun;

    [Header("Controller")]
    [SerializeField] private float slideSpeed; // tốc độ trượt tren man hinh
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(instance);
        }

    }


    private void Start()
    {
        GameManager.onGameStateChange += GameStateChangedCallBack;
    }


    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameStateChangedCallBack;
    }


    private void Update()
    {
        if(canRun)
        {
            MoveFoward();
            ManageControl();
        }
    }

    private void GameStateChangedCallBack(GameManager.GameState _gameState)
    {
        if (_gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if(_gameState == GameManager.GameState.GameOver || _gameState == GameManager.GameState.LevelComplate)
        {
            StopMoving();
        }    
    }


    private void StartMoving()
    {
        canRun = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canRun = false; 
        playerAnimator.Idle();
    }

    private void MoveFoward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

    }    


    private void ManageControl()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }    

        else if(Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            // Giữ nguyên vị trí z để không ảnh hưởng đến MoveForward()
            Vector3 newPosition = clickedPlayerPosition + Vector3.right * xScreenDifference;
            newPosition.z = transform.position.z; // Giữ nguyên giá trị trục z
            newPosition.x = Mathf.Clamp(newPosition.x, -5f + crowdSystem.GetCrowdRadius(), 5f -  crowdSystem.GetCrowdRadius());

            transform.position = newPosition;

        }    
    }    
}
