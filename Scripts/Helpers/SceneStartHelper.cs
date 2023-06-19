using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneStartHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private UnityEvent OnSceneStart;

    private void Start()
    {
        OnSceneStart.Invoke();
    }
}
