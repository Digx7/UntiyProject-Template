using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Vector3 Event Channel")]
public class Vector3EventChannelSO : ScriptableObject
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 vector)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(vector);
        }
    }
}