using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthEnemy : MonoBehaviour
{
    public int maxHealth = 100 ; 
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " a subi " + damage + " dégâts. PV restants : " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Méthode appelée quand les PV atteignent 0
    void Die()
    {
        Debug.Log(gameObject.name + " est détruit !");
        Destroy(gameObject); // Détruit l'objet ennemi
    }
}