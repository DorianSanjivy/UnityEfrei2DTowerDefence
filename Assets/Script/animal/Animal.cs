using UnityEngine;
using System.Collections.Generic;

public class Animal : MonoBehaviour
{
    public float speed;
    private float currentSpeed;
    private List<float> activeSlowFactors = new List<float>();
    private bool isSlowed = false;

    public int health;
    public int damage;

    public int moneyDrop;
    public GameObject coinPrefab; // Reference to the coin prefab
    public float dropRadius = 1.0f; // Radius of the circle to spawn coins

    protected Transform[] pathNodes;
    protected int currentNodeIndex = 0;

    void Start()
    {
        currentSpeed = speed;
    }

    public void SetPath(Transform[] nodes)
    {
        pathNodes = nodes;
    }

    protected virtual void Update()
    {
        if (pathNodes == null || pathNodes.Length == 0) return;

        Transform targetNode = pathNodes[currentNodeIndex];
        Vector3 direction = (targetNode.position - transform.position).normalized;

        // Move towards the target node
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Check if reached the target node
        if (Vector3.Distance(transform.position, targetNode.position) < 0.1f)
        {
            currentNodeIndex++;
            if (currentNodeIndex >= pathNodes.Length)
            {
                OnReachEnd();
            }
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void AddSlowFactor(float slowFactor)
    {
        activeSlowFactors.Add(slowFactor);
        UpdateCurrentSpeed(); // Recalculate speed
    }

    public void RemoveSlowFactor(float slowFactor)
    {
        activeSlowFactors.Remove(slowFactor);
        UpdateCurrentSpeed(); // Recalculate speed
    }

    private void UpdateCurrentSpeed()
    {
        if (activeSlowFactors.Count > 0)
        {
            float maxSlowFactor = Mathf.Min(activeSlowFactors.ToArray()); // Get the strongest slow factor
            currentSpeed = speed * maxSlowFactor;
            isSlowed = true;
        }
        else
        {
            currentSpeed = speed; // Restore original speed if no slow factors
            isSlowed = false;
        }

        Debug.Log(gameObject.name + " speed updated to " + currentSpeed);
    }

    protected virtual void OnReachEnd()
    {
        Destroy(gameObject);
        GlobalVariables.grangeCurrentHealth -= damage;
        SoundManager.Play("Damage");
    }

    protected virtual void Die()
    {
        // Instantiate coins around the object
        for (int i = 0; i < moneyDrop; i++)
        {
            Vector2 randomPosition = Random.insideUnitCircle * dropRadius;
            Vector3 spawnPosition = new Vector3(transform.position.x + randomPosition.x, transform.position.y + randomPosition.y, transform.position.z);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
