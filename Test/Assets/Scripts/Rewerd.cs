
using UnityEngine;
using YG;
public class Rewards : MonoBehaviour
{
     private Test _test;
    public YandexGame sdk;
    private Tester _tester;



     private void Start()
     {
        _test = FindAnyObjectByType<Test>();
        _tester = FindAnyObjectByType<Tester>();
     }


     public void AdButton()
     {
            sdk._RewardedShow(1);
            _tester.score++;
     }

     public void ButtonCul()
     {
        _test._adsPanel.SetActive(false);
        _test._questionsPanel.SetActive(true);
     }
}

