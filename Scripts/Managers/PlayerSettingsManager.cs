using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettingsManager : Singleton<PlayerSettingsManager>
{
    public static Settings currentSettings;

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
        }
    }

    private void ProcessRequest()
    {
        if(loadFromPlayerPrefsAfterSaving)
        {
            StartCoroutine(loadAfterDelay(0.5f));
        }
    }

    private IEnumerator loadAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentSettings.Load();
    }
}
