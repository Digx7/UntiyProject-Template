using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Audio Request with Position Channel")]
public class AudioRequestWithPositionChannelSO : ScriptableObject
{
    public UnityAction<Sound, Vector3> OnRequestAudio;

    public void RaiseEvent(Sound sound, Vector3 worldPosition)
    {
        if (OnRequestAudio != null)
        {
            OnRequestAudio.Invoke(sound, worldPosition);
        }
    }
}