using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : Singleton<AudioManager>
{
    public AudioRequestChannelSO sfxChannel;
    public AudioRequestChannelSO musicChannel;
    public GameObject AudioSourcePrefab;

    private List<Sound> activeSfxs;
    private List<Sound> activeSongs;

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
        activeSfxs = new List<Sound>();
        activeSongs = new List<Sound>();
        
        StartCoroutine(UnloadStaleSounds());
    }

    // Listenter Methods ------------------------

    private void OnReciveSfxRequest(Sound sound)
    {
        Log("Recived Sfx Request for " + sound.clip.name);
        activeSfxs.Add(LoadSound(sound));
        activeSfxs[activeSfxs.Count-1].Play();
    }

    private void OnReciveMusicRequest(Sound sound)
    {
        Log("Recived music request for " + sound.clip.name);
        // Checks if the new song is the exact same as the currently
        // playing one.  If so don't load or play it
        List<Sound> allCurrentlyActiveSongs = GetAllActiveSongs();
        if(IsSongLikeThisActive(sound))
        {
            Log("Ignoring request since there" +
            " is an active song just like it");
            return;
        }

        Sound currentlyActiveSong = GetCurrentlyActiveSong();
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

    // Helper Methods ----------------------

    private Sound LoadSound(Sound sound)
    {
        GameObject gameObject = Instantiate(AudioSourcePrefab, this.transform);

        sound.source = gameObject.GetComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.outputAudioMixerGroup = sound.mixerGroup;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;

        Log("Loaded sound " + sound.clip.name);

        return sound;
    }

    private Sound GetCurrentlyActiveSong(){
        foreach (Sound song in activeSongs)
        {
            if(song.isPlaying()) return song;
        }
        return null;
    }

    private List<Sound> GetAllActiveSongs(){
        List<Sound> allActiveSongs = 
            activeSongs.FindAll(item => item.isPlaying());

        return allActiveSongs;
    }

    private bool IsSongLikeThisActive(Sound sound){
        Sound result =
            activeSongs.Find(item => item.clip.name == sound.clip.name);

        if(result != null) return true;
        return false;
    }

    private void DestroyChildrenWithCondition()
    {
        // Iterate over all children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            // Check the condition
            if (child.GetComponent<AudioSource>().isPlaying == false)
            {
                // Destroy the child object
                Destroy(child.gameObject); // or DestroyImmediate(child.gameObject) if needed
            }
        }
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
            activeSfxs.RemoveAll(item => !item.isPlaying());

            activeSongs.RemoveAll(item => !item.isPlaying());

            DestroyChildrenWithCondition();

            Log("unloaded stale sounds");

            yield return new WaitForSeconds(5);
        }
    }

    // FADE Methods --------------------------------

    private void FadeIn(Sound sound, float seconds)
    {
        Log("Starting to fade in song: " + sound.clip.name);
        StartCoroutine(FadeIn(sound.source, seconds));
    }

    private void FadeOut(Sound sound, float seconds)
    {
        Log("Starting to fade out song: " + sound.clip.name);
        StartCoroutine(FadeOut(sound.source, seconds));
    }
 
    private void CrossFade(Sound soundToFadeOut, Sound soundToFadeIn, float seconds)
    {
        Log("CrossFading between songs: " + soundToFadeOut.clip.name + 
        " -> " + soundToFadeIn.clip.name);
        FadeOut(soundToFadeOut, seconds);
        FadeIn(soundToFadeIn, seconds);
    }

}