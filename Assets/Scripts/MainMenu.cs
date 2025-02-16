using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

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

    public float delayBeforeLoad = 5f;

    [SerializeField] 
    private Image transitionImage; 
    [SerializeField] 
    private float fadeDuration = 2f;

    [SerializeField]
    private GameObject buttonPlay;
    [SerializeField]
    private GameObject buttonQuit;

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
        buttonPlay.SetActive(false);
        buttonQuit.SetActive(false);
        yield return StartCoroutine(FadeToWhite());
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadSceneAsync(1);
    }

    private IEnumerator FadeToWhite()
    {
        float elapsedTime = 0f;
        Color color = transitionImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            transitionImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }


        transitionImage.color = new Color(color.r, color.g, color.b, 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
