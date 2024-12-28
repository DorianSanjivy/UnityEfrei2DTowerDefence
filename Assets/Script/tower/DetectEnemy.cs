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

    private int damageAmount;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float fireCooldown = 0f;
    private float targetSwitchCooldown = 0f; // Cooldown for switching targets
    public float targetSwitchCooldownDuration = 2f; // Duration of the switch cooldown

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
            // Target the first enemy in range
            GameObject target = enemiesInRange[0];

            // Check if target has changed
            if (currentTarget != target)
            {
                currentTarget = target;
                targetSwitchCooldown = targetSwitchCooldownDuration; // Reset switch cooldown
                cannon.AimAt(null); // Clear the aim temporarily
                return; // Skip firing for this frame
            }

            // Only aim and fire if switch cooldown is complete
            if (targetSwitchCooldown <= 0f)
            {
                cannon.AimAt(target.transform); // Set the target in the Cannon script

                // Fire projectile if cooldown is complete
                if (fireCooldown <= 0f)
                {
                    Shoot(target);
                    fireCooldown = fireRate; // Reset fire cooldown
                }
            }
        }
        else
        {
            // Clear the cannon's target if no enemies are in range
            cannon.AimAt(null);
            currentTarget = null; // Reset current target
        }

        // Decrease cooldown timers
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;

        if (targetSwitchCooldown > 0f)
            targetSwitchCooldown -= Time.deltaTime;
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
