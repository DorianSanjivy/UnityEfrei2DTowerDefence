using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour

{
    public GameObject projectilePrefab; // Le prefab du projectile
    public float fireRate = 1f; // Temps entre chaque tir
    public Transform firePoint; // Point d'o� partent les projectiles
    private List<GameObject> enemiesInRange = new List<GameObject>(); // Liste des ennemis dans le rayon
    private float fireCooldown = 0f; // Temps restant avant le prochain tir

    void Update()
    {
        // Retirer les ennemis d�truits de la liste
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            // Cibler le premier ennemi dans la liste
            GameObject target = enemiesInRange[0];

            if (target != null)
            {

                // Tirer si le cooldown est �coul�
                if (fireCooldown <= 0f)
                {
                    Shoot(target);
                    fireCooldown = fireRate; // R�initialiser le cooldown
                }
            }
        }

        // R�duire le cooldown
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    private void Shoot(GameObject target)
    {
        if (projectilePrefab != null && firePoint != null && target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.transform.position - firePoint.position).normalized;

            // Instantiate the projectile at firePoint with proper rotation toward the target
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);

            // Assign the calculated direction to the projectile
            Projectile projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
            {
                projScript.SetDirection(direction);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("detected");
            Debug.Log("Trigger d�tect� avec : " + other.gameObject.name);
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
