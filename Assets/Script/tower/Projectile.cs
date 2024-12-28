using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;          // Speed of the projectile
    private int damage;              // Damage dealt
    private Vector3 moveDirection;   // Movement direction
    private GameObject target;

    // Set the target for the projectile
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate direction toward the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Move the projectile toward the target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            // Rotate the projectile to face the direction it's moving
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle+90);

            // Check if the projectile has reached the target
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hits its target
        if (other.gameObject == target)
        {
            Animal enemyHealth = other.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy the projectile after impact
        }
    }

    // Get the current target of the projectile
    public GameObject GetTarget()
    {
        return target;
    }
}
