using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Audio Request Channel")]
public class AudioRequestChannelSO : ScriptableObject
{
    public UnityAction<Sound> OnRequestAudio;

    public void RaiseEvent(Sound sound)
    {
        if (OnRequestAudio != null)
        {
            OnRequestAudio.Invoke(sound);
        }
    }
}