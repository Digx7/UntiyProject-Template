using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(OptionsMenu_SliderHelper))]
public class OptionsMenu_AudioMixerGroupHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string key;

    private void OnEnable()
    {
        gameObject.GetComponent<OptionsMenu_SliderHelper>().OnValueChanged += (float value) => audioMixer.SetFloat(key,value);
    }

    private void OnDisable()
    {
        gameObject.GetComponent<OptionsMenu_SliderHelper>().OnValueChanged -= (float value) => audioMixer.SetFloat(key,value);
    }
}
