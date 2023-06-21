using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettingsManager : Singleton<PlayerSettingsManager>
{
    public Settings currentSettings;
    public AudioMixer audioMixer;

    [SerializeField] private bool loadFromPlayerPrefsOnStart;
    [SerializeField] private bool loadFromPlayerPrefsAfterSaving;
    [SerializeField] private VoidEventChannelSO savePlayerPrefsRequestChannelSO;

    private void OnEnable()
    {
        savePlayerPrefsRequestChannelSO.OnEventRaised += () => ProcessRequest();
    }

    private void OnDisable()
    {
        savePlayerPrefsRequestChannelSO.OnEventRaised -= () => ProcessRequest();
    }

    private void Start()
    {
        if(loadFromPlayerPrefsOnStart)
        {
            currentSettings.Load();
            StartCoroutine(loadAfterDelay(0.1f));
        }
    }

    private void ProcessRequest()
    {
        if(loadFromPlayerPrefsAfterSaving)
        {
            StartCoroutine(loadAfterDelay(0.1f));
        }
    }

    private IEnumerator loadAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentSettings.Load();
        yield return new WaitForSeconds(seconds);
        ApplyAllSettings();
    }

    private void ApplyAllSettings()
    {
        ApplyAudioSettings();
        ApplyVideoSettings();
    }

    private void ApplyAudioSettings()
    {
        audioMixer.SetFloat(SettingsConstants.VolumeMaster, currentSettings.VolumeMaster);
        audioMixer.SetFloat(SettingsConstants.VolumeMusic, currentSettings.VolumeMusic);
        audioMixer.SetFloat(SettingsConstants.VolumeSFX, currentSettings.VolumeSFX);
        audioMixer.SetFloat(SettingsConstants.VolumeDialouge, currentSettings.VolumeDialouge);
        audioMixer.SetFloat(SettingsConstants.VolumeAmbient, currentSettings.VolumeAmbient);

        // add audio subtitles and subtitle language here

    }

    private void ApplyVideoSettings()
    {
        // Screen.SetResolution()
        QualitySettings.SetQualityLevel( (int) currentSettings.graphicsPreset, true);
    }
}
