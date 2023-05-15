using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio")]
public class Sound : ScriptableObject
{
    //public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
    public bool playOnAwake = false;
    public bool fadeInOnAwake = false;

    [HideInInspector]
    public AudioSource source;

    public void Stop()
    {
        if (source != null)
        {
            source.Stop();
        }
    }

}