using System.Collections.Generic;
using UnityEngine;

public class ZoneDamageTower : MonoBehaviour
{
    public float damageInterval = 2f; // Temps entre chaque dégâts (en secondes)
    private int damageAmount;
    private float damageCooldown = 0f;
    public Tower towerScript;

    public GameObject damageEffect; // Référence à l'effet visuel


    private List<Animal> enemiesInRange = new List<Animal>(); // Liste des ennemis dans le rayon
    void Start() { damageAmount = towerScript.damage; }
    void Update()
    {
        // Gérer le cooldown pour les dégâts
        damageCooldown -= Time.deltaTime;
        if (damageCooldown <= 0f)
        {
            TriggerDamageEffect(); // Faire apparaître l'animation
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

    // Infliger des dégâts à tous les ennemis dans le rayon
    private void DealDamage()
    {
        // Parcours inversé pour éviter les erreurs lors de la suppression
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
            // Active l'effet visuel pendant une courte durée
            damageEffect.SetActive(true);
            Invoke("DisableDamageEffect", 0.5f); // Désactive après 0.5 secondes
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
