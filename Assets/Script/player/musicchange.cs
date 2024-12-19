using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1; // Première musique
    public AudioSource music2; // Deuxième musique
    // Référence au script de gestion des PV du joueur

    private float maxHealth; // Points de vie maximum

    void Start()
    {
        // Récupère les PV maximum du joueur


        maxHealth = GlobalVariables.grangeMaxHealth;
        

        // Vérifie que les musiques sont bien assignées
        if (music1 == null || music2 == null)
        {
            Debug.LogError("Les AudioSources ne sont pas assignées !");
        }
    }

    void Update()
    {
        //if (playerHealth != null)
        //{
            // Récupère les PV actuels
            float currentHealth = GlobalVariables.grangeCurrentHealth;

        // Calcule un facteur basé sur les PV (entre 0 et 1)
            float healthFactor = currentHealth / maxHealth; ;

            // Ajuste les volumes en fonction du facteur
            if (music1 != null)
            {
                music1.volume = 1f - healthFactor; // Musique 1 baisse quand les PV augmentent
                Debug.Log("-- le son");
            }
            
            if (music2 != null)
            {
                music2.volume = healthFactor; // Musique 2 augmente quand les PV augmentent
                Debug.Log("++ le son");
            }
        //}
    }
}