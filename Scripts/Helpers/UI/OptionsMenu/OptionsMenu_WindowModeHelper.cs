using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class OptionsMenu_WindowModeHelper : CustomMonoBehaviorWrapper, ResetOptionInterface
{
    [SerializeField] private BoolEventChannelSO settingsNeedToBeSavedChannel;
    private TMP_Dropdown dropdown;
    private string[] fullScreenModes;
    private void Awake()
    {
        SetUpDropDown();

        SetStartingSetting();
    }

    private void SetUpDropDown()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();

        fullScreenModes = Enum.GetNames(typeof(FullScreenMode));
        dropdown.ClearOptions();

        dropdown.AddOptions(fullScreenModes.ToList());
    }

    private void SetStartingSetting()
    {
        if(PlayerPrefs.HasKey(SettingsConstants.WindowMode))
        {
            int currentWindowMode = PlayerPrefs.GetInt(SettingsConstants.WindowMode);
            dropdown.value = currentWindowMode;
        }
        else
        {
            Reset_Option();
        }
    }

    public void SetWindowMode(int mode)
    {
        dropdown.value = mode;

        if(PlayerPrefs.HasKey(SettingsConstants.ResolutioinHeight))
        {
            Screen.SetResolution(PlayerPrefs.GetInt(SettingsConstants.ResolutionWidth), PlayerPrefs.GetInt(SettingsConstants.ResolutioinHeight), (FullScreenMode) mode);
        }

        PlayerPrefs.SetInt(SettingsConstants.WindowMode, mode);

        settingsNeedToBeSavedChannel.RaiseEvent(true);
    }

    public void Reset_Option()
    {
        int defaultWindowMode = (int) FullScreenMode.FullScreenWindow;
        dropdown.value = defaultWindowMode;

        PlayerPrefs.SetInt(SettingsConstants.WindowMode, defaultWindowMode);
    }
}
