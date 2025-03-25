using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource ads;
    [SerializeField] private AudioClip GamePlay;
    [SerializeField] private AudioClip GameOver;
    [SerializeField] private AudioClip NextGame;


    private void Start()
    {
        ads = GetComponent<AudioSource>();
        GameManager.onGameStateChange += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChange -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
        {
            PlayGameSound();
        }
        else if (gameState == GameManager.GameState.GameOver)
        {
            GameOverSouns();
        }
        else if (gameState == GameManager.GameState.LevelComplate)
        {
            NextGameSound();
        }
    }


    private void PlayGameSound()
    {
        ads.PlayOneShot(GamePlay);
    }

    private void GameOverSouns()
    {
        ads.Stop();
        ads.PlayOneShot(GameOver);
    }

    private void NextGameSound()
    {
        ads.Stop();
        ads.PlayOneShot(NextGame);
    }
}
