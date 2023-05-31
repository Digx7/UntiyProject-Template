using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum SceneLoadType {normal, async}

[CreateAssetMenu(menuName = "ScriptableObjects/Scene")]
public class SceneSO : ScriptableObject
{
    public SceneReference sceneReference;
    public LoadSceneMode loadMode;
    public SceneLoadType loadType;
    public UnityEvent OnSceneStart;
}