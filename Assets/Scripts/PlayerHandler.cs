using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    private Animator animator;
    private bool dsIsOpen = false;
    [SerializeField] private GameObject NintendoDs;
    [SerializeField] private float moveDuration = 0.2f; // Temps pour l'animation

    private Vector3 dsOpenPosition;
    private Vector3 dsClosedPosition;
    bool bIsActive = false;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void InitNintendoPosition()
    {
        Debug.Log("!!!!!!!!!!!!!!");
        dsOpenPosition = NintendoDs.transform.localPosition;
        dsClosedPosition = dsOpenPosition + new Vector3(0, -1f, 0);
        NintendoDs.transform.localPosition = dsClosedPosition;
        Debug.Log(dsOpenPosition);
    }

    public void SetActive(bool active)
    {
        bIsActive = active;
        GetComponentInChildren<Camera>().enabled = active;
        GetComponentInChildren<AudioListener>().enabled = active;
        GetComponent<PlayerMovement>().enabled = active;
        NintendoDs.GetComponent<AudioSource>().enabled = active;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && bIsActive)
        {
            Debug.Log("DS");
            if (dsIsOpen)
            {
                Debug.Log("Closing");
                animator.SetBool("isClosing", true);
                animator.SetBool("isOpening", false);
                StartCoroutine(MoveDs(dsClosedPosition));
                dsIsOpen = false;
            }
            else
            {
                Debug.Log("Opening");
                animator.SetBool("isOpening", true);
                animator.SetBool("isClosing", false);
                StartCoroutine(MoveDs(dsOpenPosition));
                dsIsOpen = true;
            }
        }

    }

    private IEnumerator MoveDs(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = NintendoDs.transform.localPosition;

        while (elapsedTime < moveDuration)
        {
            NintendoDs.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame
        }

        NintendoDs.transform.localPosition = targetPosition; // S'assurer qu'on atteint bien la position cible
    }

    
}
