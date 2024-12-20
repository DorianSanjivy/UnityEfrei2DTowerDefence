using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1; // Premi�re musique
    public AudioSource music2; // Deuxi�me musique
    public AudioSource music3;


    private float maxHealth; // Points de vie maximum

    void Start()
    {
        // R�cup�re les PV maximum du joueur


        maxHealth = GlobalVariables.grangeMaxHealth;
        music1.volume = 0f;
        music2.volume = 1f;
        music3.volume = 0f;

        // V�rifie que les musiques sont bien assign�es
        if (music1 == null || music2 == null || music3 == null)
        {
            Debug.LogError("Les AudioSources ne sont pas assign�es !");
        }
    }

    void Update()
    {
        
            float currentHealth = GlobalVariables.grangeCurrentHealth;

        // Calcule un facteur bas� sur les PV (entre 0 et 1)
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