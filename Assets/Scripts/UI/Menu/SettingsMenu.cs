using System;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private SliderSoundChanger _soundChanger;

    private void Start()
    {
        _soundChanger = GetComponentInChildren<SliderSoundChanger>();
    }

    private void OnDisable()
    {
        SaveSettings();
    }

    public event Action<float> SettingsChanged;

    private void SaveSettings()
    {
        if (_soundChanger)
            PlayerPrefs.SetFloat("Volume", _soundChanger.SoundValue);
    }
}