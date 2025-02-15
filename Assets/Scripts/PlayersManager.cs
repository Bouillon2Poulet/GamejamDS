using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;

    private GameObject CurrentPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentPlayer = Player1;
        Player1.GetComponent<PlayerHandler>().SetActive(true);
        Player2.GetComponent<PlayerHandler>().SetActive(false);

        Player1.GetComponent<PlayerMovement>().SetPlayerBehaviorVariable(1);
        Player2.GetComponent<PlayerMovement>().SetPlayerBehaviorVariable(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Changing players");
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        if (CurrentPlayer == Player1)
        {
            CurrentPlayer = Player2;
            Player2.GetComponent<PlayerHandler>().SetActive(true);
            Player1.GetComponent<PlayerHandler>().SetActive(false);
        }
        else
        {
            CurrentPlayer = Player1;
            Player1.GetComponent<PlayerHandler>().SetActive(true);
            Player2.GetComponent<PlayerHandler>().SetActive(false);
        }
    }
}
