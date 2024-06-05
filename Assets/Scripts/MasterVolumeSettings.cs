using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterVolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Button _muteButton;

    private bool _isMuted;

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _muteButton.onClick.AddListener(ToggleMute);
    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        _muteButton.onClick.RemoveListener(ToggleMute);
    }

    private void Start()
    {
        ChangeVolume(_volumeSlider.value);
    }

    private void ToggleMute() 
    {
        _isMuted = !_isMuted;

        if (_isMuted)
            _audioMixerGroup.audioMixer.SetFloat(
            MixerExposedParams.MasterVolume, -80);
        else
            ChangeVolume(_volumeSlider.value);
    }

    private void ChangeVolume(float sliderValue)
    {
        if (_isMuted)
            return;

        float soundLevel = Mathf.Clamp(sliderValue, 0.0001f, 1);
        float volume = Mathf.Log10(soundLevel) * 20;

        _audioMixerGroup.audioMixer.SetFloat(
            MixerExposedParams.MasterVolume, volume);
    }
}
