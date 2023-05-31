using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Int Event Channel")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int number)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(number);
        }
    }
}