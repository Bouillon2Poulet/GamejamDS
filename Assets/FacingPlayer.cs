using UnityEngine;

public class FacingPlayer : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    private GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialisation du target avec le joueur actuel
        target = GameManager.GetComponent<PlayersManager>().CurrentPlayer;

        // S'abonner à l'événement OnCurrentPlayerChanged pour mettre à jour le target lorsqu'il change
        PlayersManager.OnCurrentPlayerChanged += UpdateTarget;
    }

    // Cette méthode sera appelée lorsqu'un joueur change
    private void UpdateTarget()
    {
        // Mettre à jour le target avec le joueur actuel
        target = GameManager.GetComponent<PlayersManager>().CurrentPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        // Tourner l'objet pour regarder le target
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    // Pense à te désabonner à l'événement lorsque l'objet est détruit pour éviter des fuites de mémoire
    private void OnDestroy()
    {
        PlayersManager.OnCurrentPlayerChanged -= UpdateTarget;
    }
}
