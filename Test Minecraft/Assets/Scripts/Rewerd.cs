
using UnityEngine;
using YG;
public class Rewards : MonoBehaviour
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
     }

     public void ButtonCul()
     {
        _test._adsPanel.SetActive(false);
        _test._questionsPanel.SetActive(true);
     }
}

