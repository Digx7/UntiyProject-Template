using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MainMenu, Gameplay, WinScreen, LoseScreen
    }
    
    public GameState currentGameState {get; private set;}

    public void setCurrentGameState(GameState newState)
    {
        currentGameState = newState;
    }
}