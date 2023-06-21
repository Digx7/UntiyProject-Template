using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class OptionsMenu_ResolutionHelper : CustomMonoBehaviorWrapper, ResetOptionInterface
{
    [SerializeField] private BoolEventChannelSO settingsNeedToBeSavedChannel;
    private TMP_Dropdown dropdown;
    private Resolution[] resolutions;
    
    private void Awake()
    {
        SetUpDropDown();

        SetStartingSetting();
    }

    private void SetUpDropDown()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();

        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        dropdown.ClearOptions();

        foreach (Resolution resolution in resolutions)
        {
            options.Add(resolution.width + "x" + resolution.height);
        }

        dropdown.AddOptions(options);
    }

    private void SetStartingSetting()
    {
        if(PlayerPrefs.HasKey(SettingsConstants.ResolutioinHeight))
        {
            string currentResolution = (PlayerPrefs.GetInt(SettingsConstants.ResolutionWidth) + "x" + PlayerPrefs.GetInt(SettingsConstants.ResolutioinHeight));
            dropdown.value = dropdown.options.FindIndex(optionData => optionData.text == currentResolution);
        }
        else
        {
            Reset_Option();
        }
    }

    public void SetResolution(int index)
    {
        FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;
        if(PlayerPrefs.HasKey(SettingsConstants.WindowMode))
        {
            fullScreenMode = (FullScreenMode) PlayerPrefs.GetInt(SettingsConstants.WindowMode);
        }
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullScreenMode);
        
        PlayerPrefs.SetInt(SettingsConstants.ResolutionWidth, resolutions[index].width);
        PlayerPrefs.SetInt(SettingsConstants.ResolutioinHeight, resolutions[index].height);

        settingsNeedToBeSavedChannel.RaiseEvent(true);
    }

    public void Reset_Option()
    {
        FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;
        
        if(PlayerPrefs.HasKey(SettingsConstants.WindowMode))
        {
            fullScreenMode = (FullScreenMode) PlayerPrefs.GetInt(SettingsConstants.WindowMode);
        }
        Screen.SetResolution(1920, 1080, fullScreenMode);

        PlayerPrefs.SetInt(SettingsConstants.ResolutionWidth, 1920);
        PlayerPrefs.SetInt(SettingsConstants.ResolutioinHeight, 1080);
    }
}
