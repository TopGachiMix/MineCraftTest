using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLanguages : MonoBehaviour
{
    
    [SerializeField] public int _languages;


    void Start()
    {
        PlayerPrefs.GetInt("Language", _languages);
    }
    public void RussianLanguages()
    {
        _languages = 0;
        PlayerPrefs.SetInt("Language", _languages);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EnglishLanguages()
    {
        _languages = 1;
        PlayerPrefs.SetInt("Language", _languages);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
