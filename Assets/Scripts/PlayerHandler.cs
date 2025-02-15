using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Animator animator;
    private bool dsIsOpen = false;
    [SerializeField] private GameObject NintendoDs;
    [SerializeField] private float moveDuration = 0.5f; // Temps pour l'animation

    private Vector3 dsOpenPosition;
    private Vector3 dsClosedPosition;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        // Définir les positions d'ouverture et de fermeture
        dsOpenPosition = NintendoDs.transform.position;
        dsClosedPosition = dsOpenPosition + new Vector3(0, -2f, 0);
    }

    public void SetActive(bool active)
    {
        GetComponentInChildren<Camera>().enabled = active;
        GetComponentInChildren<AudioListener>().enabled = active;
        GetComponent<PlayerMovement>().enabled = active;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("DS");
            if (dsIsOpen)
            {
                Debug.Log("Closing");
                StartCoroutine(MoveDs(dsClosedPosition));
                animator.SetBool("isClosing", true);
                animator.SetBool("isOpening", false);
                dsIsOpen = false;
            }
            else
            {
                Debug.Log("Opening");
                StartCoroutine(MoveDs(dsOpenPosition));
                animator.SetBool("isOpening", true);
                animator.SetBool("isClosing", false);
                dsIsOpen = true;
            }
        }
    }

    private IEnumerator MoveDs(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = NintendoDs.transform.position;

        while (elapsedTime < moveDuration)
        {
            NintendoDs.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame
        }

        NintendoDs.transform.position = targetPosition; // S'assurer qu'on atteint bien la position cible
    }
}
