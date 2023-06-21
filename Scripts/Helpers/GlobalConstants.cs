using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Languages {English, Español}

public enum Resolutions {_1920x1080, _1366x768, _360x640}

public enum WindowMode {Fullscreen, Windowed, Boarderless}
public enum VideoQuality {Ultra, VeryHigh, High, Low, VeryLow}

public enum Difficulty {easy, medium, hard}

public static class GlobalConstants 
{
    
}

public static class SettingsConstants
{
    public static string VolumeMaster = "VolumeMaster";
    public static string VolumeMusic = "VolumeMusic";
    public static string VolumeSFX = "VolumeSFX";
    public static string VolumeDialouge = "VolumeDialouge";
    public static string VolumeAmbient = "VolumeAmbient";
    public static string ShowSubtitles = "ShowSubtitles";
    public static string SubtitleLangague = "SubtitleLangague";

    public static string Resolution = "Resolution";
    public static string WindowMode = "WindowMode";
    public static string GraphicsPreset = "GraphicsPreset";

    public static string Difficulty = "Difficulty";
    public static string ShowHints = "ShowHints";
}