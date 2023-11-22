using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private SliderSoundChanger _soundChanger;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _soundChanger.SoundValueChanged += SetVolumeValue;
    }

    private void SetVolumeValue(float volume)
    {
        _audioSource.volume = volume;
    }
}