using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/String Event Channel")]
public class StringEventChannelSO : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string word)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(word);
        }
    }
}
