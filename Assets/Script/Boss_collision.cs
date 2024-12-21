using UnityEngine;

public class BossCollision : MonoBehaviour
{
    public string playerTag = "Player"; // Le tag du joueur (assurez-vous que le joueur a ce tag)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si le boss entre en collision avec le joueur
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Afficher le Game Over
            TriggerGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Alternative pour les colliders en mode "Is Trigger"
        if (other.CompareTag(playerTag))
        {
            TriggerGameOver();
        }
    }

    /// <summary>
    /// Affiche l'écran Game Over et arrête le jeu.
    /// </summary>
    private void TriggerGameOver()
    {
        // Affiche le canvas Game Over
        GameObject.Find("Canvas").transform.Find("Game_Over").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("GameOverImage").gameObject.SetActive(true);

        // Optionnel : Arrêter le temps
        Time.timeScale = 0;

        Debug.Log("Le boss a touché le joueur. Game Over !");
    }
}