using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitTheGame : MonoBehaviour
{
    public void QuitTheApp()
    {
        Debug.Log("a quitt� le jeu");
        Application.Quit();
    }
}
