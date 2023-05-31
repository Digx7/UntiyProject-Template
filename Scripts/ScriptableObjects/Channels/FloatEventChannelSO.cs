using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Float Event Channel")]
public class FloatEventChannelSO : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float number)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(number);
        }
    }
}