using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState { Menu, Game, LevelComplate, GameOver}

    private GameState gameState;
    public static Action<GameState> onGameStateChange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance.gameObject);

        Debug.Log("Game State at Awake: " + gameState);
    }


    public void SetGameState(GameState _GameState)
    {
        this.gameState = _GameState;
        onGameStateChange?.Invoke(gameState);
        Debug.Log("Trang thai game hien tai" +  gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }    

    public bool IsLevelComplate()
    {
        return gameState == GameState.LevelComplate;
    }

    public bool IsGameOver()
    {
        return gameState == GameState.GameOver;
    }
        
}
