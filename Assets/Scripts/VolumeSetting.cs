using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private MixerExposedParams _mixerExposedParam;
    [SerializeField] private Slider _volumeSlider;

    public MixerExposedParams MixerExposedParam => _mixerExposedParam;

    public event Action<MixerExposedParams, float> VolumeChanged;

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(OnChangedVolume);
    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(OnChangedVolume);
    }

    private void Start()
    {
        UpdateVolume();
    }

    public void UpdateVolume()
        => VolumeChanged(_mixerExposedParam, _volumeSlider.value);

    private void OnChangedVolume(float value)
        => VolumeChanged(_mixerExposedParam, value);
}