using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameStateChannelSO gameStateChannel;

    private void OnEnable()
    {
        gameStateChannel.OnEventRaised += (gameState) => gameState.OnState.Invoke();
    }

    private void OnDisable()
    {
        gameStateChannel.OnEventRaised -= (gameState) => gameState.OnState.Invoke();
    }
}