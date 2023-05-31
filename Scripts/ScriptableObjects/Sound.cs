using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio")]
public class Sound : ScriptableObject
{
    //public string name;
    public AudioClip clip;
    public AudioMixerGroup mixerGroup; 
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;

    public void Play(){
        if(source != null && !isPlaying()){
            source.Play();
        }
    }

    public void Stop()
    {
        if (source != null)
        {
            source.Stop();
        }
    }

    public bool isPlaying(){
        if (source != null)
            return source.isPlaying;
        else return false;
    }

    public void TryTearDown(){
        if (source != null && !source.isPlaying){
            Destroy(source.gameObject);
        }
    }

}