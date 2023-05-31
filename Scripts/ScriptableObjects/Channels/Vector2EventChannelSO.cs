using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Vector2 Event Channel")]
public class Vector2EventChannelSO : ScriptableObject
{
    public UnityAction<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 vector)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(vector);
        }
    }
}