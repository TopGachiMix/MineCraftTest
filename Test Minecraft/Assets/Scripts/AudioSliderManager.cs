using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderManager : MonoBehaviour
{
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private AudioSource _audioMusicSource;
    [SerializeField] private AudioSource _audioSoundSource;
    [SerializeField] private GameObject _musBg;
    [SerializeField] private Text _textVolumeMusic;
    [SerializeField] private Text _textVolumeSound;

    public void MusicVolumeManager()
{
    _audioMusicSource.volume = _sliderMusic.value;
    PlayerPrefs.SetFloat("MusicVolume", _audioMusicSource.volume);
    UpdateVolumeText(); // Обновляем текст громкости
}

public void SoundVolumeManager()
{
    _audioSoundSource.volume = _sliderSound.value;
    PlayerPrefs.SetFloat("SoundVolume", _audioSoundSource.volume);
    UpdateVolumeText(); // Обновляем текст громкости
}

private void UpdateVolumeText()
{
    _textVolumeMusic.text = "Музыка: " + (_audioMusicSource.volume * 100).ToString("F0") + "%";
    _textVolumeSound.text = "Звуки: " + (_audioSoundSource.volume * 100).ToString("F0") + "%";

    if(_audioMusicSource.volume == 0)
        _textVolumeMusic.text = "Музыка: Выкл".ToString();
    
    if(_audioSoundSource.volume == 0)
        _textVolumeSound.text = "Звуки: Выкл".ToString();

}

private void Start()
{
    DontDestroyOnLoad(_musBg);
    
    float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f); 
    float savedSoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f); 

    _sliderMusic.value = savedMusicVolume;
    _sliderSound.value = savedSoundVolume;

    _audioMusicSource.volume = savedMusicVolume;
    _audioSoundSource.volume = savedSoundVolume;

    UpdateVolumeText(); // Обновляем текст при старте

    _sliderMusic.onValueChanged.AddListener(delegate { MusicVolumeManager(); });
    _sliderSound.onValueChanged.AddListener(delegate { SoundVolumeManager(); });
}

private void OnApplicationQuit()
{
    // Сохраняем значения громкости при выходе
    PlayerPrefs.SetFloat("MusicVolume", _audioMusicSource.volume);
    PlayerPrefs.SetFloat("SoundVolume", _audioSoundSource.volume);
    PlayerPrefs.Save();
}
}