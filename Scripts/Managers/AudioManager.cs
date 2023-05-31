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

    private void OnEnable()
    {
        musicChannel.OnRequestAudio += ((sound) => OnReciveMusicRequest(sound));
        sfxChannel.OnRequestAudio += ((sound) => OnReciveSfxRequest(sound));
    }

    private void OnDisable()
    {
        musicChannel.OnRequestAudio -= ((sound) => OnReciveMusicRequest(sound));
        sfxChannel.OnRequestAudio -= ((sound) => OnReciveSfxRequest(sound));
    }

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        StartCoroutine(UnloadStaleSounds());
    }

    // Listenter Methods ------------------------

    private void OnReciveSfxRequest(Sound sound)
    {
        activeSfxs.Add(LoadSound(sound));
        activeSfxs[activeSfxs.Count-1].Play();
    }

    private void OnReciveMusicRequest(Sound sound)
    {
        // Checks if the new song is the exact same as the currently
        // playing one.  If so don't load or play it
        Sound currentlyActiveSong = GetCurrentlyActiveSong();
        if(currentlyActiveSong != null && currentlyActiveSong.clip.name == sound.clip.name)
        {
            return;
        }

        Sound newSong = LoadSound(sound);
        activeSongs.Add(newSong);

        if(currentlyActiveSong != null)
        {
            CrossFade(currentlyActiveSong, newSong, 5.0f);
        }
        else
        {
            FadeIn( newSong , 5.0f );
        }
    }

    // Setup and Get Functions ----------------------

    private Sound LoadSound(Sound sound)
    {
        GameObject gameObject = Instantiate(AudioSourcePrefab, this.transform);

        sound.source = gameObject.GetComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.outputAudioMixerGroup = sound.mixerGroup;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.playOnAwake = sound.playOnAwake;

        return sound;
    }

    public Sound GetCurrentlyActiveSong(){
        foreach (Sound song in activeSongs)
        {
            if(song.isPlaying()) return song;
        }
        return null;
    }

    // IEnumerators ---------------------------------------

    private IEnumerator FadeIn(AudioSource source, float seconds)
    {
        if(source.isPlaying)
        {
            yield return null;
        }
        
        float targetVolume = source.volume;
        float currentTime = 0f;
        source.volume = 0.0f;

        source.Play();

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

        source.Stop();
    }

    private IEnumerator UnloadStaleSounds()
    {
        while(1 == 1){
            foreach (Sound sound in activeSfxs)
            {
                if(!sound.isPlaying()){
                    Destroy( sound.source.gameObject );
                }
            }

            activeSfxs.RemoveAll(item => !item.isPlaying());

            foreach (Sound sound in activeSongs)
            {
                if(!sound.isPlaying()){
                    Destroy( sound.source.gameObject );
                }
            }

            activeSongs.RemoveAll(item => !item.isPlaying());

            yield return new WaitForSeconds(30.0f);
        }
    }

    // FADE Methods --------------------------------

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