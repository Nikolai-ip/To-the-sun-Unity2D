using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private SliderSoundChanger _soundChanger;

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
