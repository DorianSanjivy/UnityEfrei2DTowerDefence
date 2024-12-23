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
    void Start() { 
        towerScript = GetComponent<Tower>(); 
        damageAmount = towerScript.damage; 
        damageInterval = towerScript.rate; 
    }
    void Update()
    {
        // G�rer le cooldown pour les d�g�ts
        damageCooldown -= Time.deltaTime;
        if (damageCooldown <= 0f)
        {
            TriggerDamageEffect(); // Faire appara�tre l'animation
            DealDamage();
            damageCooldown = damageInterval;
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
                Debug.Log("hp :" + enemyHealth);
                enemiesInRange.Add(enemyHealth);
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
