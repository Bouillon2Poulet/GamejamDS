using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private NavMeshAgent monsterAgent;
    public Transform player1;
    public Transform player2;
    private Transform targetPlayer;

    public float chaseInterval = 10f; // Intervalle entre chaque recherche des joueurs
    
    private float timeSinceLastChase = 0f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        targetPlayer = player1;
        monsterAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastChase += Time.deltaTime;

        if (timeSinceLastChase >= chaseInterval)
        {
            // Trouver le joueur le plus proche
            Transform targetPlayer = GetClosestPlayer();
            
            // Fait en sorte que l'agent se dirige vers le joueur choisi
            if (targetPlayer != null)
            {
                monsterAgent.SetDestination(targetPlayer.position);
            }
            
            // Réinitialise le compteur de temps
            timeSinceLastChase = 0f;
        }

    }

    // Renvoie quel joueur est le plus proche
    Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(monsterAgent.transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(monsterAgent.transform.position, player2.position);
        
        // Compare les distances et retourne le joueur le plus proche
        if (distanceToPlayer1 < distanceToPlayer2)
        {
            return player1;
        }
        else
        {
            return player2;
        }

}
}