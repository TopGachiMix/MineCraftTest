using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tester : MonoBehaviour
{
    [SerializeField] private Text _scoreText; 
    [SerializeField] private Text _questionText;
    [SerializeField] private GameObject _bgQuestions;
    [SerializeField] private Button[] _answerButtons; // Кнопки для ответов
    [SerializeField] private Question[] _questions; // Массив вопросов
    [SerializeField] private GameObject _resultPanel; // Панель для отображения результата
    [SerializeField] public int score = 0; // Счет игрока
    [SerializeField] private Image _spriteImage; // Изображение для отображения спрайта
    [SerializeField] public GameObject _adsPanel;
    [SerializeField] public GameObject _questionsPanel;
    [SerializeField] public Animator _trueFalse;
    [SerializeField] public Button _buttonAds;
    [SerializeField] private Text _textQuestions;
    [SerializeField] private Animator _imageShow;

    private bool _isWaitingForNextQuestion = false; // Флаг ожидания следующего вопроса
    private AudioSliderManager _audioSliderManager;
    private List<int> askedQuestions = new List<int>(); // Список уже заданных вопросов
    public AudioSource _audios;

    void Start()
    {
        _resultPanel.SetActive(false); // Скрыть панель с результатом в начале
        _audioSliderManager = FindAnyObjectByType<AudioSliderManager>();
        LoadNextQuestion();
        UpdateScoreText();

        _audios = GetComponent<AudioSource>();

        // _imageShow = GetComponent<Animator>();
    }

    private void Update()
    {   
        int lastQuestionIndex = askedQuestions[askedQuestions.Count - 1];
        _textQuestions.text = $"Вопрос {askedQuestions.Count} / 100";
    }
    
    void UpdateScoreText()
    {
        _scoreText.text = $"Счёт: {score}";
    }

    public void Answer(int index)
    {
        if (_isWaitingForNextQuestion || askedQuestions.Count == 0)
        {
            return;
        }

        int lastQuestionIndex = askedQuestions[askedQuestions.Count - 1];

        if (index == _questions[lastQuestionIndex].correctAnswerIndex)
        {
            score++;
            _audioSliderManager._audioTrueQuestions.Play();
            _trueFalse.SetTrigger("green");
            if (_audioSliderManager._audioTrueQuestions)
            {
                _audios.clip = _questions[lastQuestionIndex]._audioClip;
                _audios.PlayDelayed(0.5f);
                _imageShow.SetTrigger("_isShowImage");
            }
        }
        else
        {
            _audioSliderManager._audioFalseQuestions.Play();
            
            _trueFalse.SetTrigger("red");
            _audios.clip = _questions[lastQuestionIndex]._audioClip;
            _audios.PlayDelayed(0.5f);

            StartCoroutine(ShowPanel(3));

        }

        UpdateScoreText();
        NextLevelWithDelay(lastQuestionIndex); // Используем метод задержки перехода
    }

    

    private IEnumerator ShowPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        _adsPanel.SetActive(true);
        _questionsPanel.SetActive(false);
    }
    

    public void AdsButton(int index)
    {
        if (_isWaitingForNextQuestion || askedQuestions.Count == 0)
        {
            return;
        }

    int lastQuestionIndex = askedQuestions[askedQuestions.Count - 1];
    _spriteImage.sprite = _questions[lastQuestionIndex].questionSpriteOn;
    _audios.clip = _questions[lastQuestionIndex]._audioClip;
    _audios.PlayDelayed(0.5f);

    // StartCoroutine(AdsButtonDelay());
    StartCoroutine((WaitAndLoadNext(3)));
}

IEnumerator AdsButtonDelay()
{
    _isWaitingForNextQuestion = true; // Блокируем дальнейшие взаимодействия


    yield return new WaitForSeconds(3);

    int lastQuestionIndex = askedQuestions[askedQuestions.Count - 1];
    _spriteImage.sprite = _questions[lastQuestionIndex].questionSprite;

    _isWaitingForNextQuestion = false;
    LoadNextQuestion();
}

    private IEnumerator WaitAndLoadNext(float delayTime)
    {
        _isWaitingForNextQuestion = true; // Блокируем ввод пользователя
        foreach (var btn in _answerButtons)
        {
            btn.interactable = false; // Отключаем взаимодействие с кнопками
            _buttonAds.interactable = false;
        }

        yield return new WaitForSeconds(delayTime);

        _isWaitingForNextQuestion = false; // Разблокируем вход пользователя
        foreach (var btn in _answerButtons)
        {
            btn.interactable = true; // Включаем обратно взаимодействие с кнопками
            _buttonAds.interactable = true;

        }

        LoadNextQuestion();
    }

    private void NextLevelWithDelay(int questionIndex)
    {
        // Меняем изображение на временное
        _spriteImage.sprite = _questions[questionIndex].questionSpriteOn;
        StartCoroutine(WaitAndLoadNext(5)); // Ждём 5 секунд перед загрузкой следующего вопроса
    }

    private void LoadNextQuestion()
    {
        if (askedQuestions.Count >= _questions.Length)
        {
            ShowResult(); // Показать результат, если все вопросы заданы
            _questionsPanel.SetActive(false);
            return;
        }

        int currentQuestionIndex;
        do
        {
            currentQuestionIndex = Random.Range(0, _questions.Length);
        } while (askedQuestions.Contains(currentQuestionIndex));

        askedQuestions.Add(currentQuestionIndex);
        DisplayQuestion(currentQuestionIndex);
        ChangeSprite(currentQuestionIndex);
    }

    private void ChangeSprite(int questionIndex)
    {   
        if (questionIndex < _questions.Length)
        {
            _spriteImage.sprite = _questions[questionIndex].questionSprite; // Возвращаем стандартный спрайт
        }
    }

    private void DisplayQuestion(int questionIndex)
    {
        _questionText.text = _questions[questionIndex].questionText;

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            if (i < _questions[questionIndex].answers.Length)
            {
                _answerButtons[i].GetComponentInChildren<Text>().text = _questions[questionIndex].answers[i];
                int buttonIndex = i; // Локальная переменная для замыкания
                _answerButtons[i].onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                _answerButtons[i].onClick.AddListener(() => Answer(buttonIndex));
                _answerButtons[i].gameObject.SetActive(true); // Показываем кнопку
            }
            else
            {
                _answerButtons[i].gameObject.SetActive(false); // Скрываем лишние кнопки
            }
        }
    }

    private void ShowResult()
    {
        _resultPanel.SetActive(true);
    }

    [System.Serializable]
    public class Question
    {
        public string questionText; // Текст вопроса
        public string[] answers; // Массив ответов
        public int correctAnswerIndex; // Индекс правильного ответа
        public Sprite questionSprite; // Основной спрайт для вопроса
        public Sprite questionSpriteOn; // Временный спрайт на 5 секунд
        public AudioClip _audioClip;
    }
}