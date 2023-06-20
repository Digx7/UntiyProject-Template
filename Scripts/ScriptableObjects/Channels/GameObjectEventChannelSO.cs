using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Game Object Channel")]
public class GameObjectEventChannelSO : ScriptableObject
{
    public UnityAction<GameObject> OnEventRaised;

    public void RaiseEvent(GameObject gameObject)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(gameObject);
        }
    }
}
