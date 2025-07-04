
using UnityEngine;
using YG;
public class Rewardss : MonoBehaviour
{
     private Test _test;
    public YandexGame sdk;



     private void Start()
     {
        _test = FindAnyObjectByType<Test>();
     }


     public void AdButton()
     {
            sdk._RewardedShow(1);
            _test._questionsPanel.SetActive(false);
     }

     public void ButtonCul()
     {
        _test.score++;
        _test._questionsPanel.SetActive(true);
     }
}

