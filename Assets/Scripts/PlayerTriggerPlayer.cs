
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerTriggerPlayer : MonoBehaviour
{

    public GameObject GM;
    private PlayersManager PM;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PM = GM.GetComponent<PlayersManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter (Collision other)
    {
         if (
            (PM.CurrentPlayer == PM.Player1 && other.collider.CompareTag("Player 2")) || 
         (PM.CurrentPlayer == PM.Player2) && (other.collider.CompareTag("Player 1"))
         ) {     
            Debug.Log("fin du jeu bravo");
            SceneManager.LoadSceneAsync(2);
        }
    }
}