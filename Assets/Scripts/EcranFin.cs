using UnityEngine;
using System.Collections;
using System.Collections;
using UnityEngine.UI;

public class EcranFin : MonoBehaviour
{
    public Vector3[] movePositions;
    public float[] moveDurations;    // Dur�es des translations

    public Transform[] rotationCenters; // Points autour desquels la cam�ra va tourner
    public Vector3[] rotationStartPositions; // Points de d�part de la rotation
    public float[] rotationDurations;  // Dur�e de chaque rotation
    public float rotationSpeed = 20f;  // Vitesse de rotation (degr�s/sec)

    private Vector3 firstPosition;
    private Quaternion firstRotation;

    [SerializeField]
    private Image transitionImage;
    [SerializeField]
    private float fadeDuration = 2f;

    [SerializeField]
    private GameObject buttonQuit;

    private void Start()
    {
        buttonQuit.SetActive(false);
        firstPosition = transform.position;
        firstRotation = transform.rotation;

        StartCoroutine(FadeFromWhite());
    }

    private IEnumerator FadeFromWhite()
    {
        float elapsedTime = 0f;
        Color color = transitionImage.color;

        // Assurer que l'image est totalement blanche au d�but
        transitionImage.color = new Color(color.r, color.g, color.b, 1);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            transitionImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // S'assurer que l'alpha est bien � 0 (totalement transparent)
        transitionImage.color = new Color(color.r, color.g, color.b, 0);

        buttonQuit.SetActive(true);

        // Lancer la s�quence de cam�ra une fois le fondu termin�
        StartCoroutine(CameraSequence());
    }


    private IEnumerator CameraSequence()
    {
        while (true)  // R�p�ter � l'infini jusqu'� ce que le joueur quitte
        {
            // �tape 1 : Translation vers une position interm�diaire
            yield return StartCoroutine(MoveToPosition(movePositions[0], moveDurations[0]));

            // �tape 2 : D�placer la cam�ra au point de d�part de la rotation
            transform.position = rotationStartPositions[0];

            // �tape 3 : Rotation compl�te autour du centre
            yield return StartCoroutine(RotateAroundPoint(rotationCenters[0], rotationDurations[0]));
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPos = firstPosition;
        transform.rotation = firstRotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // Assure une arriv�e pr�cise
    }

    private IEnumerator RotateAroundPoint(Transform center, float duration)
    {
        float elapsedTime = 0f;
        float anglePerSecond = 360f / duration; // Vitesse pour faire un tour complet en "duration" secondes

        while (elapsedTime < duration)
        {
            transform.RotateAround(center.position, Vector3.up, anglePerSecond * Time.deltaTime); // Rotation autour du centre
            transform.LookAt(center); // Faire en sorte que la cam�ra regarde toujours le centre

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
