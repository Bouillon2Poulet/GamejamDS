using System.Collections;
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
    bool bCanOpenDs = true;
    bool bDsWasOpenBeforeCrouching = false;

    public void SetCanOpenDsWhenCrouching(bool canOpen)
    {
        if (dsIsOpen && !canOpen)
        {
            foreach (MeshRenderer mesh in animator.transform.parent.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = false;
            }
            foreach (Canvas canvas in animator.transform.parent.GetComponentsInChildren<Canvas>())
            {
                canvas.enabled = false;
            }
            animator.SetBool("isClosing", false);
            animator.SetBool("isOpening", false);
            bDsWasOpenBeforeCrouching = true;
        }
        else if (canOpen && bDsWasOpenBeforeCrouching)
        {
            foreach (MeshRenderer mesh in animator.transform.parent.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = true;
            }
            foreach (Canvas canvas in animator.transform.parent.GetComponentsInChildren<Canvas>())
            {
                canvas.enabled = true;
            }
            dsIsOpen = true;
        }
        bCanOpenDs = canOpen;
    }


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void InitNintendoPosition()
    {
        dsOpenPosition = NintendoDs.transform.localPosition;
        dsClosedPosition = dsOpenPosition + new Vector3(0, -1f, 0);
        NintendoDs.transform.localPosition = dsClosedPosition;
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
        if (Input.GetKeyDown(KeyCode.Q) && bIsActive)
        {
            if (dsIsOpen)
            {
                animator.SetBool("isClosing", true);
                animator.SetBool("isOpening", false);
                StartCoroutine(MoveDs(dsClosedPosition));
                dsIsOpen = false;
            }
            else
            {
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
