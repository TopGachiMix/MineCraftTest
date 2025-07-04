using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Test : MonoBehaviour
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

    private AudioSliderManager _audioSliderManager;
    private List<int> askedQuestions = new List<int>(); // Список уже заданных вопросов

    void Start()
    {
        _resultPanel.SetActive(false); // Скрыть панель с результатом в начале
        _audioSliderManager = FindAnyObjectByType<AudioSliderManager>();
        LoadNextQuestion();
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        _scoreText.text = $"Счёт: {score}";
    }

    public void Answer(int index)
    {
        if (askedQuestions.Count == 0)
        {
            Debug.LogWarning("No questions have been asked yet.");
            return;
        }

        int lastQuestionIndex = askedQuestions[askedQuestions.Count - 1];

        if (index == _questions[lastQuestionIndex].correctAnswerIndex)
        {
            score++;
            _audioSliderManager._audioTrueQuestions.Play();
            _trueFalse.SetTrigger("green");
        }
        else
        {
            _audioSliderManager._audioFalseQuestions.Play();
            _adsPanel.SetActive(true);
            _questionsPanel.SetActive(false);
            _trueFalse.SetTrigger("red");
        }

        UpdateScoreText();
        LoadNextQuestion();
    }

    public void AdsButton(int index)
    {
        if (index == _questions[askedQuestions[askedQuestions.Count - 1]].correctAnswerIndex)
        {
            
            _audioSliderManager._audioTrueQuestions.Play();
        }
        else 
        {
            score++;
        }

        LoadNextQuestion();
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
            _spriteImage.sprite = _questions[questionIndex].questionSprite; // Изменяем спрайт на соответствующий вопрос
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
        public Sprite questionSprite; // Спрайт для вопроса
    }
}
