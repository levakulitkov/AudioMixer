using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIVolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Slider _volumeSlider;

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void Start()
    {
        ChangeVolume(_volumeSlider.value);
    }

    private void ChangeVolume(float sliderValue)
    {
        float soundLevel = Mathf.Clamp(sliderValue, 0.0001f, 1);
        float volume = Mathf.Log10(soundLevel) * 20;

        _audioMixerGroup.audioMixer.SetFloat(
            MixerExposedParams.UIVolume, volume);
    }
}
