using UnityEngine;

public class PlayerTriggerWithMonsterBehavior1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //TODO WENDY
        Debug.Log($"{other.gameObject.name} est entré en collision avec {gameObject.name}");
    }
}
