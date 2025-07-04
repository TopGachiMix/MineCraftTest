using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderManager : MonoBehaviour
{
    public static AudioSliderManager Instance; // Статическая ссылка на единственный экземпляр

    [SerializeField] private Slider _sliderMusic;
    [SerializeField] public Slider _sliderSound;
    [SerializeField] private AudioSource _audioMusicSource;
    [SerializeField] private AudioSource _audioSoundSource;
    [SerializeField] public AudioSource _audioTrueQuestions;
    [SerializeField] public AudioSource _audioFalseQuestions;
    [SerializeField] public AudioSource _testAudio;
    [SerializeField] private Text _textVolumeMusic;
    [SerializeField] private Text _textVolumeSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject); // Уничтожаем дубликаты
            return; // Выходим из метода, чтобы не выполнять дальнейшую инициализацию
        }
    }

    private void Start()
    {
        LoadAudioSettings(); // Загружаем настройки звука
        UpdateVolumeText(); // Обновляем текст громкости

        // Добавляем слушателей для изменения значений слайдеров
        _sliderMusic.onValueChanged.AddListener(delegate { MusicVolumeManager(); });
        _sliderSound.onValueChanged.AddListener(delegate { SoundVolumeManager(); });
    }

    private void LoadAudioSettings()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f); 
        float savedSoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f); 
        float savedSoundVolume1 = PlayerPrefs.GetFloat("SoundVolume1", 1f); 
        float savedSoundVolume2 = PlayerPrefs.GetFloat("SoundVolume2", 1f); 
        float savedSoundVolume3 = PlayerPrefs.GetFloat("testSound", 1f); 


        _sliderMusic.value = savedMusicVolume;
        _sliderSound.value = savedSoundVolume;

        _audioMusicSource.volume = savedMusicVolume;
        _audioSoundSource.volume = savedSoundVolume;
        _audioFalseQuestions.volume = savedSoundVolume2;
        _audioTrueQuestions.volume = savedSoundVolume1;
        _testAudio.volume = savedSoundVolume3;
    }

    public void MusicVolumeManager()
    {
        _audioMusicSource.volume = _sliderMusic.value; // Устанавливаем громкость музыки
        PlayerPrefs.SetFloat("MusicVolume", _audioMusicSource.volume); // Сохраняем значение
        UpdateVolumeText(); // Обновляем текст громкости музыки
    }

    public void SoundVolumeManager()
    {
        _audioSoundSource.volume = _sliderSound.value; 
        _audioFalseQuestions.volume = _sliderSound.value;
        _audioTrueQuestions.volume = _sliderSound.value;
        _testAudio.volume = _sliderSound.value;
  
        PlayerPrefs.SetFloat("SoundVolume", _audioSoundSource.volume);
        PlayerPrefs.SetFloat("SoundVolume1", _audioTrueQuestions.volume);
        PlayerPrefs.SetFloat("SoundVolume2", _audioFalseQuestions.volume);
        PlayerPrefs.SetFloat("SoundVolume3", _audioFalseQuestions.volume);
        UpdateVolumeText(); // Обновляем текст громкости звуков
    }

    private void UpdateVolumeText()
    {
        _textVolumeMusic.text = "Музыка: " + (_audioMusicSource.volume * 100).ToString("F0") + "%";
        _textVolumeSound.text = "Звуки: " + (_audioSoundSource.volume * 100).ToString("F0") + "%";

        if (_audioMusicSource.volume == 0)
            _textVolumeMusic.text = "Музыка: Выкл";

        if (_audioSoundSource.volume == 0)
            _textVolumeSound.text = "Звуки: Выкл";
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("MusicVolume", _audioMusicSource.volume);
        PlayerPrefs.SetFloat("SoundVolume", _audioSoundSource.volume);
        PlayerPrefs.SetFloat("SoundVolume1", _audioTrueQuestions.volume);
        PlayerPrefs.SetFloat("SoundVolume2", _audioFalseQuestions.volume);
        PlayerPrefs.SetFloat("SoundVolume3", _audioFalseQuestions.volume);
        PlayerPrefs.Save();
    }
}
