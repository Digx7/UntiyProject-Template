using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : Singleton<AudioManager>
{
    public List<Sound> sounds = new List<Sound>();

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;

            if (sound.playOnAwake) Play(sound.name);
            if (sound.fadeInOnAwake) FadeIn(sound.name, 5.0f);
        }
    }

    private IEnumerator FadeIn(AudioSource source, float seconds)
    {
        if(source.isPlaying)
        {
            yield return null;
        }
        
        float targetVolume = source.volume;
        float currentTime = 0f;
        source.volume = 0.0f;

        while (source.volume < targetVolume)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(0.0f, targetVolume, currentTime / seconds);

            yield return new WaitForEndOfFrame();
        }

        source.volume = targetVolume;
    }

    private IEnumerator FadeOut(AudioSource source, float seconds)
    {
        if(!source.isPlaying)
        {
            yield return null;
        }
        
        float startingVolume = source.volume;
        float currentTime = 0f;

        while (source.volume > 0)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(startingVolume, 0.0f, currentTime / seconds);

            yield return new WaitForEndOfFrame();
        }

        source.volume = 0.0f;
    }

    public void Play(string soundName)
    {
        Sound sound = sounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }

        sound.source.Play();
        if (sound.source.isPlaying)
        {
            print("now playing " + sound.name);
        }
    }

    public void PlayOneShot(string soundName)
    {
        Sound sound = sounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        print("Playing once " + sound.name);
        sound.source.PlayOneShot(sound.clip);
    }

    public void Stop(string soundName)
    {
        Sound sound = sounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }

        sound.Stop();
    }

    public void FadeIn(string soundName, float seconds)
    {
        Sound sound = sounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }

        StartCoroutine(FadeIn(sound.source, seconds));
        sound.source.Play();
        if (sound.source.isPlaying)
        {
            print("now fading in " + sound.name);
        }
    }

    public void FadeOut(string soundName, float seconds)
    {
        Sound sound = sounds.Find(item => item.name == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }

        StartCoroutine(FadeOut(sound.source, seconds));
        if (sound.source.isPlaying)
        {
            print("now fading out " + sound.name);
        }
    }
 
    public void CrossFade(string soundToFadeOut, string soundToFadeIn, float seconds)
    {
        FadeOut(soundToFadeOut, seconds);
        FadeIn(soundToFadeIn, seconds);
    }

}