using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderSoundChanger : MonoBehaviour
{
    private Slider _slider;

    private float _soundValue = 1f;

    public float SoundValue
    {
        get => _soundValue;
        set
        {
            _soundValue = value;
            SoundValueChanged?.Invoke(value);
        }
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();

        SoundValue = PlayerPrefs.GetFloat("Volume");

        _slider.onValueChanged.AddListener(delegate { SetSoundValue(); });
        _slider.minValue = 0f;
        _slider.maxValue = 1f;
        _slider.value = SoundValue;
    }

    public event Action<float> SoundValueChanged;

    private void SetSoundValue()
    {
        SoundValue = _slider.value;
    }
}