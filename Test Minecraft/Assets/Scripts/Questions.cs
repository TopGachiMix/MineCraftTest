using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; // Текст вопроса
    public string[] answers;     // Варианты ответов
    public int correctAnswerIndex; // Индекс правильного ответа
}
