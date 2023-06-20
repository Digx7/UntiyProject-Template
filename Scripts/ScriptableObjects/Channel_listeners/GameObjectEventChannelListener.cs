using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventChannelListener : MonoBehaviour
{
    [SerializeField] private GameObjectEventChannelSO gameObjectEventChannelSO;
    [SerializeField] private UnityEvent<GameObject> onHearSomething;

    private void OnEnable()
    {
        gameObjectEventChannelSO.OnEventRaised += (gameObject) => onHearSomething.Invoke(gameObject);
    }

    private void OnDisable()
    {
        gameObjectEventChannelSO.OnEventRaised -= (gameObject) => onHearSomething.Invoke(gameObject);
    }
}
