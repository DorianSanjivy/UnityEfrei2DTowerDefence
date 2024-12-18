using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;          // Vitesse du projectile
    public int damage = 25;           // D�g�ts inflig�s
    private Vector3 moveDirection;    // Direction du mouvement

    // Set the movement direction
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        // Move the projectile in the set direction
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hits the enemy
        if (other.CompareTag("Enemy"))
        {
            Animal enemyHealth = other.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy the projectile after impact
        }
    }
}
