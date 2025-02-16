using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitTheGame : MonoBehaviour
{
    public void QuitTheApp()
    {
        Debug.Log("a quitté le jeu");
        Application.Quit();
    }
}
