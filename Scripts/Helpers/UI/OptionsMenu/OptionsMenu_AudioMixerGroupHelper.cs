using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(OptionsMenu_SliderHelper))]
public class OptionsMenu_AudioMixerGroupHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string key;

    private UnityAction<float> updateAudio;

    private void Awake()
    {
        updateAudio += (float value) => audioMixer.SetFloat(key,value);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<OptionsMenu_SliderHelper>().OnValueChanged.AddListener(updateAudio);
    }

    private void OnDisable()
    {
        gameObject.GetComponent<OptionsMenu_SliderHelper>().OnValueChanged.RemoveListener(updateAudio);
    }
}
