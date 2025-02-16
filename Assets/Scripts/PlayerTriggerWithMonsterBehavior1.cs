
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerWithMonsterBehavior1 : MonoBehaviour
{

    public GameObject GM;

    private PlayersManager PM;
    public float mapSize = 75f;
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
        if (Input.GetKeyDown(KeyCode.T)) {
             transform.position = RandomNavmeshLocation();
        }
    }

    

    // Renvoie une position aléatoire dans la sphère de rayon mapSize et appartenant au NavMesh
public Vector3 RandomNavmeshLocation() {
    // Définir une taille maximale pour la recherche autour de la position actuelle.
    float maxDistance = 100f; // Taille de la zone où tu veux générer un point aléatoire.
    
    // Générer une position aléatoire dans un rayon autour de la position actuelle.
    Vector3 randomPosition = transform.position + Random.insideUnitSphere * maxDistance;

    NavMeshHit hit;
    Vector3 finalPosition = Vector3.zero;

    // Rechercher une position valide sur le NavMesh à proximité de la position générée.
    // Assurer que la recherche est dans une zone raisonnable (par exemple, la taille du monde).
    if (NavMesh.SamplePosition(randomPosition, out hit, maxDistance, NavMesh.AllAreas)) {
        finalPosition = hit.position;
    }

    return finalPosition;
}


    // Collision avec le monstre : je me tp
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster"){

            tpPosition = RandomNavmeshLocation();
            // float distanceBetweenPlayers = Vector3.Distance(tpPosition, PM.getOtherPlayer(gameObject).transform.position);

            // // Pour se faire tp loin de l'autre perso
            // while (distanceBetweenPlayers < minimumDistance) {
            //     tpPosition = RandomNavmeshLocation();
            //     distanceBetweenPlayers = Vector3.Distance(tpPosition, PM.getOtherPlayer(gameObject).transform.position);
            // }

            transform.position = tpPosition;


            if (gameObject == PM.CurrentPlayer) {
                TPSound.PlayOneShot(TP1);
            } else {
                TPSound.PlayOneShot(TP2);
            }

        }

    }
}
