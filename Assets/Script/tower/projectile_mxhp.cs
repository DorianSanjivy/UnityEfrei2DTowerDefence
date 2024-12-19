using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_mxhp : MonoBehaviour
{
    public float speed = 5f;
    
    private GameObject target;
    
    private int damage ;


    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        
    }
    public void SetDamage(int newDamage) { damage = newDamage; }
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
        else { Destroy(gameObject); }



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hits the enemy
        if (other.CompareTag("Enemy"))
        {
            Animal enemyHealth = other.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                print(damage + " dmg");
                enemyHealth.TakeDamage(damage);
               
            }

            Destroy(gameObject); // Destroy the projectile after impact
        }
    }
}
