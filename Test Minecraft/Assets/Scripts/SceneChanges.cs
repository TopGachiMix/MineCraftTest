using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanges : MonoBehaviour
{

    public void ChangeScene(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void RestartScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
