using UnityEngine;
using UnityEngine.UI;

public class NintendoRadarBehavior : MonoBehaviour
{
    [SerializeField] private Image Slot1;
    [SerializeField] private Image Slot2;
    [SerializeField] private Image Slot3;

    private Color defaultColor = Color.grey;
    public GameObject TargetNintendo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Slot1.color = defaultColor;
        Slot2.color = defaultColor;
        Slot3.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - TargetNintendo.transform.position).magnitude;
        if (distance < 50)
        {
            if (distance < 20)
            {
                if (distance < 10)
                {
                    Slot1.color = Color.green;
                    Slot2.color = Color.green;
                    Slot3.color = Color.green;
                    return;
                }
                Slot1.color = Color.yellow;
                Slot2.color = Color.yellow;
                Slot3.color = defaultColor;
                return;
            }
            Slot1.color = Color.red;
            Slot2.color = defaultColor;
            Slot3.color = defaultColor;
        }
    }
}
