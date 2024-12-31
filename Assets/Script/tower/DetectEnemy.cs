using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public GameObject projectilePrefab; // Projectile prefab
    private float fireRate;             // Time between shots
    public Transform firePoint;         // Point where projectiles are spawned
    public Cannon cannon;               // Reference to the Cannon script
    public float projectileSpeed = 5f;  // Projectile speed

    private float damageAmount;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float fireCooldown = 0f;
    private float targetSwitchCooldown = 0f; // Cooldown for switching targets
    private float targetSwitchCooldownDuration = 3f; // Duration of the switch cooldown

    private Tower towerScript;
    private GameObject currentTarget; // Keep track of the current target

    void Start()
    {
        towerScript = GetComponent<Tower>();
        damageAmount = towerScript.damage; // Get damage value from Tower script
        fireRate = towerScript.rate;
    }

    void Update()
    {
        // Remove destroyed enemies
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            GameObject target = enemiesInRange[0];

            // Switch targets with cooldown only if transitioning directly between targets
            if (currentTarget != null && currentTarget != target && targetSwitchCooldown <= 0f)
            {
                Debug.Log($"Switching target to {target.name}, resetting cooldown.");
                currentTarget = target;
                targetSwitchCooldown = targetSwitchCooldownDuration; // Reset cooldown
            }
            else if (currentTarget == null) // If no current target, immediately switch
            {
                Debug.Log($"Acquiring new target: {target.name}");
                currentTarget = target;
            }

            if (currentTarget != null && targetSwitchCooldown <= 0f)
            {
                cannon.AimAt(currentTarget.transform); // Aim at the current target

                // Fire projectile if cooldown is complete
                if (fireCooldown <= 0f)
                {
                    Shoot(currentTarget);
                    fireCooldown = fireRate; // Reset fire cooldown
                }
            }
        }
        else
        {
            // Clear the cannon's target if no enemies are in range
            cannon.AimAt(null);
            currentTarget = null; // Reset current target

            // Reset cooldowns when no enemies are present
            targetSwitchCooldown = 0f;
            fireCooldown = 0f;
        }

        // Decrease cooldown timers
        fireCooldown = Mathf.Max(0f, fireCooldown - Time.deltaTime);
        targetSwitchCooldown = Mathf.Max(0f, targetSwitchCooldown - Time.deltaTime);

        Debug.Log($"Target Switch Cooldown: {targetSwitchCooldown}");
    }

    private void Shoot(GameObject target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Set projectile velocity
            Vector3 direction = (target.transform.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

            // Set projectile properties
            Projectile projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
            {
                projScript.SetTarget(target);
                projScript.SetDamage(damageAmount); // Pass damageAmount to the projectile
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
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
