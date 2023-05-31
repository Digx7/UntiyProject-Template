using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Scene Request Channel")]
public class SceneRequestChannel : ScriptableObject
{
    public UnityAction<SceneSO> OnEventRaised;

    public void RaiseEvent(SceneSO scene)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(scene);
        }
    }
}