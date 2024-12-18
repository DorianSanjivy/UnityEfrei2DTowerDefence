using System.Collections.Generic;
using System.Collections;

using UnityEngine;

public class TTower_maxhp_target : MonoBehaviour
{
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firePoint;         // Point d'où partent les projectiles
    public float fireRate = 1f;
    public float burstDelay = 0.2f;// Temps entre chaque tir

    private float fireCooldown = 0f;    // Cooldown pour les tirs
    private List<Animal> enemiesInRange = new List<Animal>();

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f && enemiesInRange.Count > 0)
        {
            Animal target = GetTargetWithHighestHealth();
            if (target != null)
            {
                StartCoroutine(FireBurst(target.gameObject)); // Lancer les 3 tirs
                fireCooldown = fireRate; // Réinitialiser le cooldown
                
            }
        }
    }

    // Sélectionner l'ennemi avec le plus de PV
    private Animal GetTargetWithHighestHealth()
    {
        Animal bestTarget = null;
        int maxHealth = -1;

        foreach (Animal enemy in enemiesInRange)
        {
            if (enemy != null && enemy.GetCurrentHealth() > maxHealth)
            {
                maxHealth = enemy.GetCurrentHealth();
                bestTarget = enemy;
            }
        }

        return bestTarget;
    }
    IEnumerator FireBurst(GameObject target)
    {
        for (int i = 0; i < 3; i++) // Tirer 3 fois
        {
            Shoot(target); // Tirer sur la cible
            yield return new WaitForSeconds(burstDelay); // Délai entre les tirs
        }
    }
    private void Shoot(GameObject target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile_mxhp projScript = projectile.GetComponent<projectile_mxhp>();

            if (projScript != null)
            {
                projScript.SetTarget(target);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal enemyHealth = other.GetComponent<Animal>();
            if (enemyHealth != null && !enemiesInRange.Contains(enemyHealth))
            {
                enemiesInRange.Add(enemyHealth);
            }
        }
    }

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
}
