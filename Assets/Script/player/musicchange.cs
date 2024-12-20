using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1; // Premi�re musique
    public AudioSource music2; // Deuxi�me musique
    // R�f�rence au script de gestion des PV du joueur

    private float maxHealth; // Points de vie maximum

    void Start()
    {
        // R�cup�re les PV maximum du joueur


        maxHealth = GlobalVariables.grangeMaxHealth;
        

        // V�rifie que les musiques sont bien assign�es
        if (music1 == null || music2 == null)
        {
            Debug.LogError("Les AudioSources ne sont pas assign�es !");
        }
    }

    void Update()
    {
        //if (playerHealth != null)
        //{
            // R�cup�re les PV actuels
            float currentHealth = GlobalVariables.grangeCurrentHealth;

        // Calcule un facteur bas� sur les PV (entre 0 et 1)
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