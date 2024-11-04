using UnityEngine;
using UnityEngine.UI;
public class RandomTextMainMenu : MonoBehaviour
{
    [SerializeField] private int _rand;
    [SerializeField] private Text _text;
    void Start()
    {
        _rand = Random.Range(0 , 10);
        if(_rand == 0)
            _text.text = "Версия 1.0";
        else if(_rand == 1)
            _text.text = "Кубизм!";
        else if(_rand == 2)
            _text.text = "Пиксель";
        else if(_rand == 3)
            _text.text = "Ванилен";
        else if(_rand == 4)
            _text.text = "Добро пожаловать";
        else if(_rand == 5)
            _text.text = "Классика";
        else if(_rand == 6)
            _text.text = "Использует C#!";
        else if(_rand == 7)
            _text.text = "Хаха, LEL!";
        else if(_rand == 8)
            _text.text = "Рандомен!";
        else if(_rand == 9)
            _text.text = "Хмммрмм.";
    }
}
