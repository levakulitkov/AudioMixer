using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterVolumeSettings : MonoBehaviour
{
    private const int VolumeMultiplier = 20;
    private const float MinSliderValue = 1e-4f;
    private const float MaxSliderValue = 1;

    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Button _muteButton;
    [SerializeField] private List<VolumeSetting> _volumeSettings;

    private bool _isMuted;

    private void OnEnable()
    {
        _muteButton.onClick.AddListener(ToggleMute);

        _volumeSettings.ForEach(volume => volume.VolumeChanged += ChangeVolume);
    }

    private void OnDisable()
    {
        _muteButton.onClick.RemoveListener(ToggleMute);

        _volumeSettings.ForEach(volume => volume.VolumeChanged -= ChangeVolume);
    }

    private void ToggleMute()
    {
        if (_isMuted)
        {
            _isMuted = !_isMuted;

            _volumeSettings.ForEach(volume => volume.UpdateVolume());
        }
        else
        {
            ChangeVolume(MixerExposedParams.MasterVolume, 0);

            _isMuted = !_isMuted;
        }
    }

    private void ChangeVolume(MixerExposedParams param, float sliderValue)
    {
        if (_isMuted)
            return;

        float clampedSliderValue = Mathf.Clamp(sliderValue, MinSliderValue, MaxSliderValue);
        float volume = Mathf.Log10(clampedSliderValue) * VolumeMultiplier;

        _audioMixerGroup.audioMixer.SetFloat(param.ToString(), volume);
    }
}