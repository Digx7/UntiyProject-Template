using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class OptionsMenu_SliderHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private float defaultValue;
    [SerializeField] private string key;
    [SerializeField] private BoolEventChannelSO settingsNeedToBeSavedChannel;
    public UnityAction<float> OnValueChanged;
    private Slider slider;

    public void OnUpdate(float value)
    {
        PlayerPrefs.SetFloat(key, value);
        OnValueChanged.Invoke(value);
        settingsNeedToBeSavedChannel.RaiseEvent(true);
    }

    public void Reset()
    {
        OnUpdate(defaultValue);
        slider.SetValueWithoutNotify(defaultValue);
    }

    private void OnEnable()
    {
        if(slider == null)
        {
            SetUpSlider();
        }
        if(PlayerPrefs.HasKey(key))
        {
            slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(key));
        }
        else
        {
            Reset();
        }
    }

    private void SetUpSlider()
    {
        slider = gameObject.GetComponent<Slider>();
    }
}
