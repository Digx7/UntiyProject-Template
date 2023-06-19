using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    public string state;

    public UnityEvent OnState;

    // Add addtional data to be stored in the game states here
    // ...
}
