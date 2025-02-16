using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject m_hotel;
    private int startRotation = 382;
    private int endRotation = 409;

    private float speed = 0.03f;

    private float timeElapsed = 0f;
    private float duration = 2f; 

    private bool reversing = false;

    public float delayBeforeLoad = 6f;


    private void Update()
    {
        timeElapsed += Time.deltaTime * (reversing ? -speed : speed);

        float t = Mathf.Clamp01(timeElapsed / duration);

        float smoothT = Mathf.SmoothStep(0, 1, t);
        float rotationY = Mathf.Lerp(startRotation, endRotation, smoothT);

        m_hotel.transform.rotation = Quaternion.Euler(0, rotationY, 0);

        if (t >= 1f || t <= 0f)
        {
            reversing = !reversing;
        }
    }


    public void PlayGame()
    {

        StartCoroutine(BeforeLoadingScene());
    }

    private IEnumerator BeforeLoadingScene()
    {

        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
