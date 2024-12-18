using UnityEngine;
using System.Collections.Generic;

public class Slow_Tower : MonoBehaviour
{
    public float slowAmount = 0.5f; // Facteur de ralentissement (ex: 0.5 = 50% de vitesse)
    public float slowDuration = 2f; // Dur�e du ralentissement en secondes
    private List<Animal> enemiesInRange = new List<Animal>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal speed = other.GetComponent<Animal>();
            if (speed != null && !enemiesInRange.Contains(speed))
            {
                // Applique le ralentissement
                speed.ApplySlow(slowAmount, slowDuration);
                enemiesInRange.Add(speed);
                Debug.Log("ralentit" + speed);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animal speed = other.GetComponent<Animal>();
            if (speed != null)
            {
                enemiesInRange.Remove(speed);
            }
        }
    }
}
