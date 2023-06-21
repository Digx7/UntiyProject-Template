using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[RequireComponent(typeof(Toggle))]
public class OptionsMenu_ToggleHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private bool defaultValue;
    [SerializeField] private string key;
    [SerializeField] private BoolEventChannelSO settingsNeedToBeSavedChannel;
    public UnityEvent<bool> OnValueChanged;
    private Toggle toggle;

    public void OnUpdate(bool value)
    {
        if(value) 
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
        OnValueChanged.Invoke(value);
        settingsNeedToBeSavedChannel.RaiseEvent(true);
    }

    public void Reset()
    {
        OnUpdate(defaultValue);
        toggle.isOn = defaultValue;
    }

    private void OnEnable()
    {
        if(toggle == null)
        {
            SetUpToggle();
        }
        if(PlayerPrefs.HasKey(key))
        {
            int value = PlayerPrefs.GetInt(key);
            if(value == 1)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;    
            }
        }
        else
        {
            Reset();
        }
    }

    private void SetUpToggle()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }
}
