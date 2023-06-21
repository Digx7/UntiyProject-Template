using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class OptionsMenu_DropdownHelper : CustomMonoBehaviorWrapper, ResetOptionInterface
{
    [SerializeField] private int defaultValue;
    [SerializeField] private string key;
    [SerializeField] private BoolEventChannelSO settingsNeedToBeSavedChannel;
    public UnityEvent<int> OnValueChanged;
    private TMP_Dropdown dropDown;

    public void OnUpdate(int value)
    {
        PlayerPrefs.SetInt(key, value);
        OnValueChanged.Invoke(value);
        settingsNeedToBeSavedChannel.RaiseEvent(true);
    }

    public void Reset_Option()
    {
        OnUpdate(defaultValue);
        dropDown.value = defaultValue;
    }

    private void OnEnable()
    {
        if(dropDown == null)
        {
            SetUpDropdown();
        }
        if(PlayerPrefs.HasKey(key))
        {
            dropDown.value = PlayerPrefs.GetInt(key);
            OnValueChanged.Invoke(PlayerPrefs.GetInt(key));
        }
        else
        {
            Reset_Option();
        }
    }

    private void SetUpDropdown()
    {
        dropDown = gameObject.GetComponent<TMP_Dropdown>();
    }
}
