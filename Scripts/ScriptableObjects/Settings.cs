using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings")]
public class Settings : ScriptableObject
{
    [Header("Audio")]
    [Range(-80,0)]
    public float VolumeMaster;
    [Range(-80,0)]
    public float VolumeMusic;
    [Range(-80,0)]
    public float VolumeSFX;
    [Range(-80,0)]
    public float VolumeDialouge;
    [Range(-80,0)]
    public float VolumeAmbient;
    public bool ShowSubtitles;
    public Languages subtitleLanguage;


    [Header("Video")]
    public VideoQuality graphicsPreset;
    public int ScreenWidth;
    public int ScreenHeight;
    public FullScreenMode screenMode;

    [Header("Gameplay")]
    public Difficulty difficulty;
    public bool showHints;

    
    public void Load()
    {
        // load audio
        tryLoadFloat(SettingsConstants.VolumeMaster, ref VolumeMaster);
        tryLoadFloat(SettingsConstants.VolumeMusic, ref VolumeMusic);
        tryLoadFloat(SettingsConstants.VolumeSFX, ref VolumeSFX);
        tryLoadFloat(SettingsConstants.VolumeDialouge, ref VolumeDialouge);
        tryLoadFloat(SettingsConstants.VolumeAmbient, ref VolumeAmbient);
        tryLoadBool(SettingsConstants.ShowSubtitles,ref ShowSubtitles);
        tryLoadEnum<Languages>(SettingsConstants.SubtitleLangague,ref subtitleLanguage);

        // load video
        tryLoadInt(SettingsConstants.ResolutionWidth,ref ScreenWidth);
        tryLoadInt(SettingsConstants.ResolutioinHeight,ref ScreenHeight);
        tryLoadEnum<FullScreenMode>(SettingsConstants.WindowMode,ref screenMode);
        tryLoadEnum<VideoQuality>(SettingsConstants.GraphicsPreset,ref graphicsPreset);
        
        // load gameplay
        tryLoadEnum<Difficulty>(SettingsConstants.Difficulty,ref difficulty);
        tryLoadBool(SettingsConstants.ShowHints,ref showHints);

    }

    public void Save()
    {
        // load audio
        saveFloat(SettingsConstants.VolumeMaster,  VolumeMaster);
        saveFloat(SettingsConstants.VolumeMusic,  VolumeMusic);
        saveFloat(SettingsConstants.VolumeSFX,  VolumeSFX);
        saveFloat(SettingsConstants.VolumeDialouge,  VolumeDialouge);
        saveFloat(SettingsConstants.VolumeAmbient,  VolumeAmbient);
        saveBool(SettingsConstants.ShowSubtitles, ShowSubtitles);
        saveEnum<Languages>(SettingsConstants.SubtitleLangague, subtitleLanguage);

        // load video
        saveInt(SettingsConstants.ResolutionWidth, ScreenWidth);
        saveInt(SettingsConstants.ResolutioinHeight, ScreenHeight);
        saveEnum<FullScreenMode>(SettingsConstants.WindowMode, screenMode);
        saveEnum<VideoQuality>(SettingsConstants.GraphicsPreset, graphicsPreset);
        
        // load gameplay
        saveEnum<Difficulty>(SettingsConstants.Difficulty, difficulty);
        saveBool(SettingsConstants.ShowHints, showHints);
    }

    private void saveBool(string key, bool value)
    {
        if(value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    private void saveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private void saveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key,value);
    }

    private void saveEnum<T>(string key, T value)
    {
        PlayerPrefs.SetInt(key, (int) Convert.ChangeType(value, typeof(int)));
    }

    private void saveString(string key, string value)
    {
        PlayerPrefs.SetString(key,value);
    }

    private void tryLoadBool(string key, ref bool value)
    {
        if(doesPlayerPrefsHaveKey(key))
        {
            int tempValue = PlayerPrefs.GetInt(key);
            if(tempValue == 1)
            {
                value = true;
            }
            else
            {
                value = false;
            }
        }
    }

    private void tryLoadInt(string key, ref int value)
    {
        if(doesPlayerPrefsHaveKey(key))
        {
            value = PlayerPrefs.GetInt(key);
        }
    }

    private void tryLoadFloat(string key, ref float value)
    {
        if(doesPlayerPrefsHaveKey(key))
        {
            value = PlayerPrefs.GetFloat(key);
        }
    }

    private void tryLoadEnum<T>(string key, ref T value)
    {
        if(doesPlayerPrefsHaveKey(key))
        {
            value = (T) Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key));
        } 
    }

    private void tryLoadString(string key, ref string value)
    {
        if(doesPlayerPrefsHaveKey(key))
        {
            value = PlayerPrefs.GetString(key);
        }
    }

    private bool doesPlayerPrefsHaveKey(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
