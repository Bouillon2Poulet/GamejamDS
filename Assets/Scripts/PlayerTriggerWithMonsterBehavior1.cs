
using UnityEngine.AI;
using UnityEngine;

public class PlayerTriggerWithMonsterBehavior1 : MonoBehaviour
{

    public GameObject GM;

    private PlayersManager PM;
    public float minimumDistance = 50f;

    private Vector3 tpPosition; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PM = GM.GetComponent<PlayersManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Renvoie une position aléatoire dans la sphère de rayon radius et appartenant au NavMesh
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomPosition = Random.insideUnitSphere * radius;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomPosition, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    // Collision avec le monstre : je me tp
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster"){

            tpPosition = RandomNavmeshLocation(145f);

            // Pour se faire tp loin de l'autre perso
            while (Vector3.Distance(tpPosition, PM.getOtherPlayer(gameObject).transform.position) < minimumDistance) {
                tpPosition = RandomNavmeshLocation(145f);
            }

            transform.position = tpPosition;
    }}
}
