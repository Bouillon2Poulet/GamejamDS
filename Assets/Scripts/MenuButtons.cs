using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    private float scaleFactor = 1.1f; 
    private float speed = 5f; 

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * scaleFactor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(targetScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(targetScale * 1.05f)); 
        StartCoroutine(ScaleTo(targetScale, 0.1f));  
    }

    private IEnumerator ScaleTo(Vector3 target, float returnSpeed = -1f)
    {
        float t = 0f;
        float animSpeed = returnSpeed > 0 ? returnSpeed : speed;

        while (t < 1f)
        {
            t += Time.deltaTime * animSpeed;
            transform.localScale = Vector3.Lerp(transform.localScale, target, t);
            yield return null;
        }
    }
}
