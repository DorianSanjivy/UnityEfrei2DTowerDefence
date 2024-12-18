using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour_zone : MonoBehaviour

{
    public GameObject projectilePrefab; // Le prefab du projectile
    public float fireRate = 1f; // Temps entre chaque tir
    public Transform firePoint; // Point d'où partent les projectiles
    public float rotationSpeed = 5f; // Vitesse de rotation vers la cible

    private List<GameObject> enemiesInRange = new List<GameObject>(); // Liste des ennemis dans le rayon
    private float fireCooldown = 0f; // Temps restant avant le prochain tir

    void Update()
    {
        // Retirer les ennemis détruits de la liste
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            // Cibler le premier ennemi dans la liste
            GameObject target = enemiesInRange[0];

            if (target != null)
            {
                // Tirer si le cooldown est écoulé
                if (fireCooldown <= 0f)
                {
                    Shoot(target);
                    fireCooldown = fireRate; // Réinitialiser le cooldown
                }
            }
        }

        // Réduire le cooldown
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    private void Shoot(GameObject target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Créer le projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Donner une direction au projectile
            Projectile_zone projScript = projectile.GetComponent<Projectile_zone>();
            if (projScript != null && target != null)
            {
                projScript.SetTarget(target);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("detected");
            Debug.Log("Trigger détecté avec : " + other.gameObject.name);
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}