using UnityEngine;
using System.Collections.Generic;

public class Slow_Tower : MonoBehaviour
{
    private float slowAmount; // Slow factor (e.g., 0.5 = 50% speed)
    private List<Animal> enemiesInRange = new List<Animal>();

    private Tower towerScript;

    void Start() {
        towerScript = GetComponent<Tower>(); 
        slowAmount = towerScript.rate; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal animal = other.GetComponent<Animal>();
            if (animal != null && !enemiesInRange.Contains(animal))
            {
                enemiesInRange.Add(animal);
                animal.AddSlowFactor(slowAmount); // Add this tower's slow factor
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal animal = other.GetComponent<Animal>();
            if (animal != null && enemiesInRange.Contains(animal))
            {
                enemiesInRange.Remove(animal);
                animal.RemoveSlowFactor(slowAmount); // Remove this tower's slow factor
            }
        }
    }
}
