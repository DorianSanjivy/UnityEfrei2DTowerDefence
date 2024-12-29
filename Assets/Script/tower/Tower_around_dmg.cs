using System.Collections.Generic;
using UnityEngine;

public class ZoneDamageTower : MonoBehaviour
{
    private float damageInterval; // Temps entre chaque d�g�ts (en secondes)
    private float damageAmount;
    private float damageCooldown = 0f;
    private Tower towerScript;

    public GameObject damageEffect; // R�f�rence � l'effet visuel
    private List<Animal> enemiesInRange = new List<Animal>(); // Liste des ennemis dans le rayon

    private bool countdownActive = false; // To track if the countdown is active

    //private Vector3 originalPosition; // Store the original position of the tower
    //private float shakeIntensity = 0.5f; // Base intensity of the shake
    //private float shakeFrequency = 10f; // Frequency of the shake

    void Start() { 
        towerScript = GetComponent<Tower>(); 
        damageAmount = towerScript.damage; 
        damageInterval = towerScript.rate; 
        //originalPosition = transform.position; // Store the original position
    }
    void Update()
    {
        // Progress the cooldown timer
        if (countdownActive)
        {
            damageCooldown -= Time.deltaTime;

            // Apply shaking effect
            //float intensityMultiplier = 1 - (damageCooldown / damageInterval); // Increases as cooldown nears 0
            //float shakeOffset = Mathf.Sin(Time.time * shakeIntensity) * shakeFrequency * intensityMultiplier;
            //transform.position = originalPosition + new Vector3(shakeOffset, 0, 0);


            if (damageCooldown <= 0f)
            {
                TriggerDamageEffect(); // Show the animation
                DealDamage();

                // Reset countdownActive and check for enemies
                countdownActive = false;

                // If enemies are still in range, immediately reactivate the countdown
                if (enemiesInRange.Count > 0)
                {
                    countdownActive = true;
                    damageCooldown = damageInterval; // Reset cooldown
                }
            }
        }
        else
        {
            // If there are enemies and countdown is inactive, start it
            if (enemiesInRange.Count > 0)
            {
                countdownActive = true;
                damageCooldown = damageInterval; // Start the countdown
            }
        }
    }

    // Quand un ennemi entre dans le rayon
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal enemyHealth = other.GetComponent<Animal>();
            if (enemyHealth != null && !enemiesInRange.Contains(enemyHealth))
            {
                Debug.Log("Enemy entered: " + enemyHealth);
                enemiesInRange.Add(enemyHealth);

                // Start the countdown if it's not already active
                if (!countdownActive)
                {
                    countdownActive = true;
                    damageCooldown = damageInterval; // Start the initial countdown
                }
            }
        }
    }

    // Quand un ennemi sort du rayon
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal enemyHealth = other.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                enemiesInRange.Remove(enemyHealth);
            }
        }
    }

    // Infliger des d�g�ts � tous les ennemis dans le rayon
    private void DealDamage()
    {
        // Parcours invers� pour �viter les erreurs lors de la suppression
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] != null)
            {   Debug.Log(enemiesInRange[i] + "took dmg" );
                enemiesInRange[i].TakeDamage(damageAmount);
                
            }
            else
            {
                // Retirer les ennemis null de la liste
                enemiesInRange.RemoveAt(i);
            }
        }
    }
    private void TriggerDamageEffect()
    {
        // Trouver le script Rotation_bras et activer la rotation
        Rotation_bras rotationScript = GetComponentInChildren<Rotation_bras>();
        if (rotationScript != null)
        {
            rotationScript.StartRotation();
        }
    }
}
