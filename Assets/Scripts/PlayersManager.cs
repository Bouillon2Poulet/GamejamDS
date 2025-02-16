using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public GameObject CurrentPlayer;

    public static event Action OnCurrentPlayerChanged;

    private AudioSource switchSound;

    public Animator anim;

    public GameObject Monster;

    public Image voileBlanc;

    [SerializeField] private GameObject UI_verte;
    [SerializeField] private GameObject UI_roz;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switchSound = gameObject.GetComponent<AudioSource>();
        CurrentPlayer = Player1;
        Player1.GetComponent<PlayerHandler>().SetActive(true);
        Player2.GetComponent<PlayerHandler>().SetActive(false);
        UI_roz.SetActive(true);
        UI_verte.SetActive(false);

        Player1.GetComponent<PlayerMovement>().SetPlayerBehaviorVariable(1);
        Player2.GetComponent<PlayerMovement>().SetPlayerBehaviorVariable(2);

        Player1.GetComponent<PlayerHandler>().InitNintendoPosition();
        Player2.GetComponent<PlayerHandler>().InitNintendoPosition();


        Player1.GetComponentInChildren<NintendoRadarBehavior>().TargetNintendo = Player2;
        Player2.GetComponentInChildren<NintendoRadarBehavior>().TargetNintendo = Player1;

        Player1.GetComponentInChildren<MessageHandlerNintendo>().NintendoPlayerIndex = 1;
        Player2.GetComponentInChildren<MessageHandlerNintendo>().NintendoPlayerIndex = 2;

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


        if (Vector3.Distance(CurrentPlayer.transform.position, Monster.transform.position) < 6f)
        {
            // Calculer la distance entre le joueur et le monstre
            float distance = Vector3.Distance(CurrentPlayer.transform.position, Monster.transform.position);

            // Calculer l'alpha en fonction de la distance
            // Plus la distance est petite, plus l'alpha est proche de 1
            float alpha = 1 - (distance / 6f);

            // Clamper l'alpha entre 0 et 1
            alpha = Mathf.Clamp(alpha, 0f, 1f);

            // Appliquer la nouvelle couleur avec l'alpha calculé
            voileBlanc.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            voileBlanc.color = new Color(1, 1, 1, 0f);
        }
    }

    void SwitchPlayer()
    {
        if (CurrentPlayer == Player1)
        {
            CurrentPlayer = Player2;
            Player2.GetComponent<PlayerHandler>().SetActive(true);
            Player1.GetComponent<PlayerHandler>().SetActive(false);
            UI_roz.SetActive(false);
            UI_verte.SetActive(true);
        }
        else
        {
            CurrentPlayer = Player1;
            Player1.GetComponent<PlayerHandler>().SetActive(true);
            Player2.GetComponent<PlayerHandler>().SetActive(false);
            UI_roz.SetActive(true);
            UI_verte.SetActive(false);
        }

        switchSound.Play();
        anim.Play("Flash", -1, 0f);
        OnCurrentPlayerChanged.Invoke();
    }

    public int getPlayerId()
    {
        return (CurrentPlayer == Player1 ? 1 : 2);
    }

    public GameObject getOtherPlayer(GameObject Player)
    {
        return (Player == Player1 ? Player2 : Player1);
    }

}
