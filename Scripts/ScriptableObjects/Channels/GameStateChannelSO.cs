using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Game State Channel")]
public class GameStateChannelSO : ScriptableObject
{
    public UnityAction<GameState> OnEventRaised;

    public void RaiseEvent(GameState gameState)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(gameState);
        }
    }
}