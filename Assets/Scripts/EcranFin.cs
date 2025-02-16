using UnityEngine;
using System.Collections;
using System.Collections;
using UnityEngine.UI;

public class EcranFin : MonoBehaviour
{
    public Vector3[] movePositions;
    public float[] moveDurations;    // Durées des translations

    public Transform[] rotationCenters; // Points autour desquels la caméra va tourner
    public Vector3[] rotationStartPositions; // Points de départ de la rotation
    public float[] rotationDurations;  // Durée de chaque rotation
    public float rotationSpeed = 20f;  // Vitesse de rotation (degrés/sec)

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

        // Assurer que l'image est totalement blanche au début
        transitionImage.color = new Color(color.r, color.g, color.b, 1);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            transitionImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // S'assurer que l'alpha est bien à 0 (totalement transparent)
        transitionImage.color = new Color(color.r, color.g, color.b, 0);

        buttonQuit.SetActive(true);

        // Lancer la séquence de caméra une fois le fondu terminé
        StartCoroutine(CameraSequence());
    }


    private IEnumerator CameraSequence()
    {
        while (true)  // Répéter à l'infini jusqu'à ce que le joueur quitte
        {
            // Étape 1 : Translation vers une position intermédiaire
            yield return StartCoroutine(MoveToPosition(movePositions[0], moveDurations[0]));

            // Étape 2 : Déplacer la caméra au point de départ de la rotation
            transform.position = rotationStartPositions[0];

            // Étape 3 : Rotation complète autour du centre
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

        transform.position = target; // Assure une arrivée précise
    }

    private IEnumerator RotateAroundPoint(Transform center, float duration)
    {
        float elapsedTime = 0f;
        float anglePerSecond = 360f / duration; // Vitesse pour faire un tour complet en "duration" secondes

        while (elapsedTime < duration)
        {
            transform.RotateAround(center.position, Vector3.up, anglePerSecond * Time.deltaTime); // Rotation autour du centre
            transform.LookAt(center); // Faire en sorte que la caméra regarde toujours le centre

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
