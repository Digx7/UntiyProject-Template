using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class OptionsMenu_GraphicsQualityHelper : CustomMonoBehaviorWrapper, ResetOptionInterface
{
    [SerializeField] private BoolEventChannelSO settingsNeedToBeSavedChannel;
    private TMP_Dropdown dropdown;
    private string[] qualityLevels;

    private void Awake()
    {
        SetUpDropDown();
        SetStartingSetting();
    }

    private void SetUpDropDown()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();

        qualityLevels = QualitySettings.names;
        dropdown.ClearOptions();

        dropdown.AddOptions(qualityLevels.ToList());
    }

    private void SetStartingSetting()
    {
        if(PlayerPrefs.HasKey(SettingsConstants.GraphicsPreset))
        {
            int currentQuality = PlayerPrefs.GetInt(SettingsConstants.GraphicsPreset);
            dropdown.value = currentQuality; 
        }
        else
        {
            Reset_Option();
        }
    }

    public void SetGraphicsQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        PlayerPrefs.SetInt(SettingsConstants.GraphicsPreset, level);
        settingsNeedToBeSavedChannel.RaiseEvent(true);
    }

    public void Reset_Option()
    {
        int defaultQuality = dropdown.options.Count() - 1;    // Gets the highest possible quality level for the current platform

        dropdown.value = defaultQuality;
        PlayerPrefs.SetInt(SettingsConstants.GraphicsPreset, defaultQuality);
    }
}

    
