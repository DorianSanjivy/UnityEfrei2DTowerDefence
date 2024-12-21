using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1; // Première musique
    public AudioSource music2; // Deuxième musique
    public AudioSource music3;

    private float maxHealth; // Points de vie maximum
    public bool isMuted = false; // Mute flag

    void Start()
    {
        // Récupère les PV maximum du joueur
        maxHealth = GlobalVariables.grangeMaxHealth;

        // Initial music volumes
        music1.volume = 0f;
        music2.volume = 1f;
        music3.volume = 0f;

        // Vérifie que les musiques sont bien assignées
        if (music1 == null || music2 == null || music3 == null)
        {
            Debug.LogError("Les AudioSources ne sont pas assignées !");
        }
    }

    void Update()
    {
        // Check the mute state
        if (isMuted)
        {
            // Set all volumes to 0 when muted
            if (music1 != null) music1.volume = 0;
            if (music2 != null) music2.volume = 0;
            if (music3 != null) music3.volume = 0;
            return; // Exit the update function
        }

        float currentHealth = GlobalVariables.grangeCurrentHealth;

        // Calculate a health factor (between 0 and 1)
        float healthFactor = currentHealth / maxHealth;

        if (currentHealth > 0)
        {
            // Adjust volumes based on health factor
            if (music1 != null)
            {
                music1.volume = 1f - healthFactor; // Music 1 decreases as health increases
            }

            if (music2 != null)
            {
                music2.volume = healthFactor; // Music 2 increases as health increases
            }
        }
        else
        {
            // If health is 0 or below
            if (music1 != null) music1.volume = 0;
            if (music2 != null) music2.volume = 0;
            if (music3 != null) music3.volume = 1f; // Play music3 at full volume
        }
    }

    // Function to toggle mute
    public void ToggleMute()
    {
        isMuted = !isMuted;
    }
}
