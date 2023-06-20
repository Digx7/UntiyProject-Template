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
    public Resolutions resolution;
    public WindowMode windowMode;
    public VideoQuality graphicsPreset;
    public VideoQuality textureQuality;
    public VideoQuality shadowsQuality;
    public VideoQuality reflectionsQuality;
    public VideoQuality effectsQuality;
    public VideoQuality antiAliasingQuality;
    public bool useVSync;
    public VideoQuality fieldOfViewQuality;
    public VideoQuality ambientOcclusionQuality;
    public VideoQuality depthOfFieldQuality;
    public float gamma;


    [Header("Gameplay")]
    public Difficulty difficulty;
    public bool showHints;

    
    public void Load()
    {
        // load audio
        tryLoadFloat("Volume_Master", ref VolumeMaster);
        tryLoadFloat("Volume_Music", ref VolumeMaster);
        tryLoadFloat("Volume_Music", ref VolumeMusic);
        tryLoadFloat("Volume_SFX", ref VolumeSFX);
        tryLoadFloat("Volume_Dialouge", ref VolumeDialouge);
        tryLoadFloat("Volume_Ambient", ref VolumeAmbient);
        tryLoadBool("Show_Subtitles",ref ShowSubtitles);
        tryLoadEnum<Languages>("SubtitleLangague",ref subtitleLanguage);

        // load video
        tryLoadEnum<Resolutions>("Resolution", ref resolution);
        tryLoadEnum<WindowMode>("Window_Mode",ref windowMode);
        tryLoadEnum<VideoQuality>("Graphics_Preset",ref graphicsPreset);
        tryLoadEnum<VideoQuality>("Graphics_texture",ref textureQuality);
        tryLoadEnum<VideoQuality>("Graphics_shadow",ref shadowsQuality);
        tryLoadEnum<VideoQuality>("Graphics_reflection",ref reflectionsQuality);
        tryLoadEnum<VideoQuality>("Graphics_effects",ref effectsQuality);
        tryLoadEnum<VideoQuality>("Graphics_anti_aliasing",ref antiAliasingQuality);
        tryLoadBool("Graphics_V_Sync",ref useVSync);
        tryLoadEnum<VideoQuality>("Graphics_field_of_view",ref fieldOfViewQuality);
        tryLoadEnum<VideoQuality>("Graphics_ambient_occlusion",ref ambientOcclusionQuality);
        tryLoadEnum<VideoQuality>("Graphics_depth_of_field",ref depthOfFieldQuality);
        tryLoadFloat("Graphics_Gamma",ref gamma);

        // load gameplay
        tryLoadEnum<Difficulty>("Gameplay_Difficulty",ref difficulty);
        tryLoadBool("Gameplay_Show_Hints",ref showHints);

    }

    public void Save()
    {
        // saves the data into the PlayerPrefs;
        saveFloat("Volume_Master",  VolumeMaster);
        saveFloat("Volume_Music",  VolumeMaster);
        saveFloat("Volume_Music",  VolumeMusic);
        saveFloat("Volume_SFX",  VolumeSFX);
        saveFloat("Volume_Dialouge",  VolumeDialouge);
        saveFloat("Volume_Ambient",  VolumeAmbient);
        saveBool("Show_Subtitles", ShowSubtitles);
        saveEnum<Languages>("SubtitleLangague", subtitleLanguage);

        // load video
        saveEnum<Resolutions>("Resolution",  resolution);
        saveEnum<WindowMode>("Window_Mode", windowMode);
        saveEnum<VideoQuality>("Graphics_Preset", graphicsPreset);
        saveEnum<VideoQuality>("Graphics_texture", textureQuality);
        saveEnum<VideoQuality>("Graphics_shadow", shadowsQuality);
        saveEnum<VideoQuality>("Graphics_reflection", reflectionsQuality);
        saveEnum<VideoQuality>("Graphics_effects", effectsQuality);
        saveEnum<VideoQuality>("Graphics_anti_aliasing", antiAliasingQuality);
        saveBool("Graphics_V_Sync", useVSync);
        saveEnum<VideoQuality>("Graphics_field_of_view", fieldOfViewQuality);
        saveEnum<VideoQuality>("Graphics_ambient_occlusion", ambientOcclusionQuality);
        saveEnum<VideoQuality>("Graphics_depth_of_field", depthOfFieldQuality);
        saveFloat("Graphics_Gamma", gamma);

        // load gameplay
        saveEnum<Difficulty>("Gameplay_Difficulty", difficulty);
        saveBool("Gameplay_Show_Hints", showHints);
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
            value = (T) Convert.ChangeType(PlayerPrefs.GetInt(key), typeof(T));
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
