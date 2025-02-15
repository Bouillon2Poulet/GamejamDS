using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayersManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public GameObject CurrentPlayer;

    public static event Action OnCurrentPlayerChanged;

    private AudioSource switchSound;

    public TextMeshProUGUI commandsText;
    public Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switchSound = gameObject.GetComponent<AudioSource>();
        CurrentPlayer = Player1;
        Player1.GetComponent<PlayerHandler>().SetActive(true);
        Player2.GetComponent<PlayerHandler>().SetActive(false);

        Player1.GetComponent<PlayerMovement>().SetPlayerBehaviorVariable(1);
        Player2.GetComponent<PlayerMovement>().SetPlayerBehaviorVariable(2);

        OnCurrentPlayerChanged.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Changing players");
            SwitchPlayer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reload");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SwitchPlayer()
    {
        if (CurrentPlayer == Player1)
        {
            CurrentPlayer = Player2;
            Player2.GetComponent<PlayerHandler>().SetActive(true);
            Player1.GetComponent<PlayerHandler>().SetActive(false);
            commandsText.text ="(E) : changer <br> (Ctrl) : s'accroupir <br> (A) : ouvrir la DS";
        }
        else
        {
            CurrentPlayer = Player1;
            Player1.GetComponent<PlayerHandler>().SetActive(true);
            Player2.GetComponent<PlayerHandler>().SetActive(false);
            commandsText.text ="(E) : changer <br> (Espace) x2 : double saut <br> (A) : ouvrir la DS";
        }

        switchSound.Play();
        anim.Play("Flash", -1, 0f);
        OnCurrentPlayerChanged.Invoke();
    }

    public int getPlayerId() {
        return (CurrentPlayer == Player1 ? 1 : 2);
    }

    public GameObject getOtherPlayer(GameObject Player) {
        return (Player == Player1 ? Player2 : Player1);
    }

}
