using System.Collections.Generic;
using UnityEngine;

public class ZoneDamageTower : MonoBehaviour
{
    private float damageInterval; // Temps entre chaque d�g�ts (en secondes)
    private int damageAmount;
    private float damageCooldown = 0f;
    private Tower towerScript;

    public GameObject damageEffect; // R�f�rence � l'effet visuel
    private List<Animal> enemiesInRange = new List<Animal>(); // Liste des ennemis dans le rayon

    private bool countdownActive = false; // To track if the countdown is active

    void Start() { 
        towerScript = GetComponent<Tower>(); 
        damageAmount = towerScript.damage; 
        damageInterval = towerScript.rate; 
    }
    void Update()
    {
        // Progress the cooldown timer
        if (countdownActive)
        {
            damageCooldown -= Time.deltaTime;

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
        if (damageEffect != null)
        {
            // Active l'effet visuel pendant une courte dur�e
            damageEffect.SetActive(true);
            Invoke("DisableDamageEffect", 0.5f); // D�sactive apr�s 0.5 secondes
        }
    }

    private void DisableDamageEffect()
    {
        if (damageEffect != null)
        {
            damageEffect.SetActive(false);
        }
    }

}
