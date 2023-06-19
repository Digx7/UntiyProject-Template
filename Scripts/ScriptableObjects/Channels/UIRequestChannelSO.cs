using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/UI Request Channel")]
public class UIRequestChannelSO : ScriptableObject
{
    public UnityAction<UIRequest> OnEventRaised;

    public void RaiseEvent(UIRequest uIRequest)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(uIRequest);
        }
    }
}