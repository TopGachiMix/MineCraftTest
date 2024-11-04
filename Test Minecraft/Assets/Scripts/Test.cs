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
    [SerializeField] private Text _resultText; // Текст для отображения результата
    [SerializeField] private GameObject _scoreObj;
    [SerializeField] private GameObject _scoreObj2;
    [SerializeField] private GameObject _questionsButton;
    [SerializeField] private Text _endTest;
    [SerializeField] private int score = 0; // Счет игрока

    
    private AudioSliderManager _audioSliderManager;
    private List<int> askedQuestions = new List<int>(); // Список уже заданных вопросов

    void Start()
    {
        _resultPanel.SetActive(false); // Скрыть панель с результатом в начале
        LoadNextQuestion();
        _audioSliderManager = FindAnyObjectByType<AudioSliderManager>();
    }

    void Update()
    {
        _scoreText.text = $"Счёт: {score}";
    }

    public void Answer(int index)
    {
        if (index == _questions[askedQuestions[askedQuestions.Count - 1]].correctAnswerIndex)
        {
            score++;
            _audioSliderManager._audioTrueQuestions.Play();
        }
        else
        {
            _audioSliderManager._audioFalseQuestions.Play();
        }

        LoadNextQuestion();
    }

    private void LoadNextQuestion()
    {
        if (askedQuestions.Count >= _questions.Length)
        {
            ShowResult(); // Показать результат, если все вопросы заданы
            return;
        }

        int currentQuestionIndex;
        do
        {
            currentQuestionIndex = Random.Range(0, _questions.Length);
        } while (askedQuestions.Contains(currentQuestionIndex));

        askedQuestions.Add(currentQuestionIndex);
        DisplayQuestion(currentQuestionIndex);
    }

    private void DisplayQuestion(int questionIndex)
    {
        _questionText.text = _questions[questionIndex].questionText;

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            if (i < _questions[questionIndex].answers.Length)
            {
                _answerButtons[i].GetComponentInChildren<Text>().text = _questions[questionIndex].answers[i];
                _answerButtons[i].gameObject.SetActive(true);
                int index = i; // Локальная переменная для замыкания
                _answerButtons[i].onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                _answerButtons[i].onClick.AddListener(() => Answer(index)); // Добавляем новый слушатель
            }
            else
            {
                _answerButtons[i].gameObject.SetActive(false); // Скрыть лишние кнопки
            }
        }
    }

    private void ShowResult()
    {
        _questionText.gameObject.SetActive(false); 
        foreach (var button in _answerButtons)
        {
            button.gameObject.SetActive(false); 
        }

        _resultPanel.SetActive(true);
        _scoreObj.SetActive(true);
        _scoreObj2.SetActive(false);
        _questionsButton.SetActive(false);

        if(score >= 0 && score < 10)
            _resultText.text =  $"Ты только начинаешь свой путь в этом удивительном кубическом мире! Каждый шаг - это возможность узнать что-то новое. Продолжай исследовать, и твои знания будут расти!".ToString();
        else if(score >= 10 && score < 20)
            _resultText.text = $"Ты на правильном пути! Твои знания о Майнкрафте начинают укрепляться. Продолжай учиться, и вскоре ты сможешь справляться с любыми вызовами!".ToString();
        else if(score >= 20 && score < 30)
            _resultText.text =  $"Отличные результаты! Ты уже знаешь много о Майнкрафте и уверенно движешься вперед. Продолжай в том же духе, и ты станешь настоящим мастером!".ToString();
        else if(score >= 30 && score < 50)
            _resultText.text = $"Ты на высоте! Твои навыки и знания о Майнкрафт впечатляют. Ты готов к новым приключениям и вызовам, которые ждут впереди!".ToString();
        else if(score >= 50 && score < 70)
            _resultText.text = $"Просто потрясающе! Ты обладаешь глубокими знаниями и навыками, которые помогут тебе справляться с любыми трудностями. Впереди только самые захватывающие приключения!".ToString();
        else if(score >= 70 && score < 90)
            _resultText.text = $"Ты настоящий эксперт! Твои знания о Майнкрафт безграничны, и ты уверенно преодолеваешь все преграды. Ты вдохновляешь других своим мастерством!".ToString();
        else if(score >= 90)
            _resultText.text = $"Ты достиг вершины мастерства! Ты — настоящий герой этого кубического мира. Твои знания и навыки вдохновляют, и впереди у тебя только самые захватывающие приключения!".ToString();


        _endTest.text = $"Вы ответили верно на {score} из {_questions.Length} вопросов".ToString();
        

    }
}
