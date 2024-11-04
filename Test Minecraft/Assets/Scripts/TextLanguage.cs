using UnityEngine.UI;
using UnityEngine;

public class TextLanguage : MonoBehaviour
{
    [SerializeField] private int _languages;
    [SerializeField] private string[] _textLines;
    [SerializeField] private Text _text;
    private ManagerLanguages _lan;
    void Start()
    {
        _text = GetComponent<Text>();
        _languages = PlayerPrefs.GetInt("Languages", _languages);
        _text.text = "" + _textLines[_languages];

        _lan = FindAnyObjectByType<ManagerLanguages>();
    }

    void Update()
    {
        if(_lan._languages == 0)
            _languages = 0;
        else if(_lan._languages == 1)
            _languages = 1;
    }


}
