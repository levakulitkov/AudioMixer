using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class AudioButton : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _audioSource.Play();
    }
}