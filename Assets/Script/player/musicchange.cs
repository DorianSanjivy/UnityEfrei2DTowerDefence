using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1; // Première musique
    public AudioSource music2; // Deuxième musique
    public AudioSource music3;


    private float maxHealth; // Points de vie maximum

    void Start()
    {
        // Récupère les PV maximum du joueur


        maxHealth = GlobalVariables.grangeMaxHealth;
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
        
            float currentHealth = GlobalVariables.grangeCurrentHealth;

        // Calcule un facteur basé sur les PV (entre 0 et 1)
            float healthFactor = currentHealth / maxHealth; ;
        if (currentHealth > 0)
        {
            // Ajuste les volumes en fonction du facteur
            if (music1 != null)
            {
                music1.volume = 1f - healthFactor; // Musique 1 baisse quand les PV augmentent

            }

            if (music2 != null)
            {
                music2.volume = healthFactor; // Musique 2 augmente quand les PV augmentent

            }
        }
        else { music1.volume = 0 ;
            music2.volume = 0;
            music3.volume = 100;
        }
        //}
    }
}