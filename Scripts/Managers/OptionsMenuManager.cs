using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuManager : MenuManager
{
    [SerializeField] private MenuScreen ApplyChangesScreen; 
    [SerializeField] private bool doPlayerPrefsNeedToBeSaved;
    [SerializeField] private BoolEventChannelSO doPlayerPrefsNeedToBeSavedChannel;
    [SerializeField] private VoidEventChannelSO saveChangesRequestChannel;

    private MenuScreen previousScreen;
    

    protected override void OnEnable()
    {
        base.OnEnable();
        doPlayerPrefsNeedToBeSavedChannel.OnEventRaised += (value) => doPlayerPrefsNeedToBeSaved = value;
        saveChangesRequestChannel.OnEventRaised += SavePlayerPrefs;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        doPlayerPrefsNeedToBeSavedChannel.OnEventRaised -= (value) => doPlayerPrefsNeedToBeSaved = value;
        saveChangesRequestChannel.OnEventRaised -= SavePlayerPrefs;
    }

    protected override void OnBack()
    {
        if(currentScreen.name == ApplyChangesScreen.name)
        {
            OnChangeScreen(previousScreen);
        }
        else if(!doPlayerPrefsNeedToBeSaved)
        {
            base.OnBack();
        } 
        else
        {
            previousScreen = currentScreen;
            OnChangeScreen(ApplyChangesScreen);
        }
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
        doPlayerPrefsNeedToBeSaved = false;
    }
}
