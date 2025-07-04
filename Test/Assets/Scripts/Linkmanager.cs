using UnityEngine;

public class Linkmanager : MonoBehaviour
{
    
   
   public void LinkManage(string Url)
   {
        Application.OpenURL(Url);
   }
}
