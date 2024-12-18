using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_mxhp : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 25;
    private GameObject target;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
           Vector3 direction = (target.transform.position - transform.position).normalized;
           transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Destroy(gameObject);
        } 
        }

        
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
