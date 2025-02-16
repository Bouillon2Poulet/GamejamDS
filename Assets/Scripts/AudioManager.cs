using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicPlayer1; // Source audio pour la musique de Player 1
    public AudioSource musicPlayer2; // Source audio pour la musique de Player 2
    public AudioSource musicMonster; // Source audio pour la musique du monstre

    public float fadeDuration = 2f; // Durée du fondu en secondes
    public float maxDistance = 20f; // Distance maximale pour entendre la musique du monstre

    private PlayersManager playersManager; // Référence au PlayersManager
    private Transform playerTransform; // Transform du joueur actif
    private Transform monsterTransform; // Transform du monstre

    void Start()
    {
        // Trouver le PlayersManager dans la scène
        playersManager = FindObjectOfType<PlayersManager>();

        if (playersManager == null)
        {
            Debug.LogError("PlayersManager not found in the scene!");
            return;
        }

        if (playersManager.CurrentPlayer == null)
        {
            Debug.LogError("CurrentPlayer is not assigned in PlayersManager!");
            return;
        }

        // Trouver les transforms du joueur et du monstre
        playerTransform = playersManager.CurrentPlayer.transform;
        monsterTransform = GameObject.FindGameObjectWithTag("Monster").transform;

        if (playerTransform == null || monsterTransform == null)
        {
            Debug.LogError("Player or Monster transform is null!");
            return;
        }

        // Démarrer les trois musiques en boucle
        musicPlayer1.loop = true;
        musicPlayer2.loop = true;
        musicMonster.loop = true;

        // Jouer les trois musiques
        musicPlayer1.Play();
        musicPlayer2.Play();
        musicMonster.Play();

        // Initialiser les volumes en fonction du joueur actif
        UpdateMusicVolumes();
    }

    void Update()
    {
        playerTransform = playersManager.CurrentPlayer.transform;

        // Mettre à jour les volumes en fonction du joueur actif
        UpdateMusicVolumes();

        // Mettre à jour le volume de la musique du monstre en fonction de la distance
        UpdateMonsterMusicVolume();
    }

    void UpdateMusicVolumes()
    {
        if (playersManager != null)
        {
            if (playersManager.getPlayerId() == 1)
            {
                // Fade in pour Player 1, Fade out pour Player 2
                StartCoroutine(FadeAudio(musicPlayer1, 1f, fadeDuration)); // Fade in
                StartCoroutine(FadeAudio(musicPlayer2, 0f, fadeDuration)); // Fade out
            }
            else if (playersManager.getPlayerId() == 2)
            {
                // Fade in pour Player 2, Fade out pour Player 1
                StartCoroutine(FadeAudio(musicPlayer2, 1f, fadeDuration)); // Fade in
                StartCoroutine(FadeAudio(musicPlayer1, 0f, fadeDuration)); // Fade out
            }
        }
    }

    void UpdateMonsterMusicVolume()
    {
        if (playerTransform != null && monsterTransform != null)
        {

            // Calculer la distance entre le joueur et le monstre
            float distance = Vector3.Distance(playerTransform.position, monsterTransform.position);

            // Calculer le volume en fonction de la distance
            float volume = 1f - Mathf.Clamp01(distance / maxDistance);

            // Appliquer le volume à la musique du monstre
            musicMonster.volume = volume;
        }
    }

    // Coroutine pour effectuer un fondu audio
    private System.Collections.IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurer que le volume final est exact
        audioSource.volume = targetVolume;
    }
}