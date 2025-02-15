
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerWithMonsterBehavior1 : MonoBehaviour
{

    public GameObject GM;

    private PlayersManager PM;
    private float mapSize = 70f;
    private float minimumDistance;

    private AudioSource TPSound;
    public AudioClip TP1;
    public AudioClip TP2;



    private Vector3 tpPosition; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PM = GM.GetComponent<PlayersManager>();
        minimumDistance = mapSize/3f;
        TPSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Renvoie une position aléatoire dans la sphère de rayon mapSize et appartenant au NavMesh
    public Vector3 RandomNavmeshLocation() {
        Vector3 randomPosition = Random.insideUnitSphere * mapSize;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomPosition, out hit, mapSize, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

    // Collision avec le monstre : je me tp
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster"){

            tpPosition = RandomNavmeshLocation();

            // Pour se faire tp loin de l'autre perso
            while (Vector3.Distance(tpPosition, PM.getOtherPlayer(gameObject).transform.position) < minimumDistance) {
                tpPosition = RandomNavmeshLocation();
            }

            transform.position = tpPosition;
            if (gameObject == PM.CurrentPlayer) {
                TPSound.PlayOneShot(TP1);
            } else {
                TPSound.PlayOneShot(TP2);
            }

        }
    }
}
