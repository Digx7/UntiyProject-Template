using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class OptionsMenu_GraphicsResetHelper : CustomMonoBehaviorWrapper
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();

        string[] names = QualitySettings.names;
        dropdown.ClearOptions();

        dropdown.AddOptions(names.ToList());
    }

    public void SetGraphicsQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
    }
}

    
