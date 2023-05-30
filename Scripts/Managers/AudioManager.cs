using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : Singleton<AudioManager>
{
    public AudioRequestChannelSO sfxChannel;
    public AudioRequestChannelSO musicChannel;

    public List<Sound> activeSfxs;
    public List<Sound> activeSongs;

    public GameObject AudioSourcePrefab;

    private void OnEnable(){
        musicChannel.OnRequestAudio += ((sound) => onReciveMusicRequest(sound));
        sfxChannel.OnRequestAudio += ((sound) => onReciveSfxRequest(sound));
    }

    private void OnDisable(){
        musicChannel.OnRequestAudio -= ((sound) => onReciveMusicRequest(sound));
        sfxChannel.OnRequestAudio -= ((sound) => onReciveSfxRequest(sound));
    }

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        
    }

    private void onReciveSfxRequest(Sound sound){
        activeSfxs.Add(LoadSound(sound));
        activeSfxs[activeSfxs.Count - 1].Play();
    }

    private void onReciveMusicRequest(Sound sound){
        activeSongs.Add(LoadSound(sound));
        FadeIn(activeSongs[activeSongs.Count - 1], 3.0f);
    }

    private Sound LoadSound(Sound sound){
        GameObject gameObject = Instantiate(AudioSourcePrefab, this.transform);

        sound.Setup(gameObject.GetComponent<AudioSource>());

        return sound;
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

    private IEnumerator UnloadStaleSounds(){
        while(1 == 1){
            foreach (Transform item in transform)
            {
                
            }
        }
    }

    public void FadeIn(Sound sound, float seconds)
    {
        StartCoroutine(FadeIn(sound.source, seconds));
    }

    public void FadeOut(Sound sound, float seconds)
    {
        StartCoroutine(FadeOut(sound.source, seconds));
    }
 
    public void CrossFade(Sound soundToFadeOut, Sound soundToFadeIn, float seconds)
    {
        FadeOut(soundToFadeOut, seconds);
        FadeIn(soundToFadeIn, seconds);
    }

}