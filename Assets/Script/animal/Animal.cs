using UnityEngine;
using System.Collections.Generic;

public class Animal : MonoBehaviour
{
    public float speed;
    private float currentSpeed;
    private List<float> activeSlowFactors = new List<float>();
    private bool isSlowed = false;

    public int health;
    private int maxHealth;
    public int damage;

    public int moneyDrop;
    public GameObject coinPrefab; // Reference to the coin prefab
    public float dropRadius = 1.0f; // Radius of the circle to spawn coins

    public GameObject lifeBarPrefab; // Prefab for the life bar
    private GameObject lifeBarInstance; // Instance of the life bar
    private Transform lifeBarForeground; // Reference to the foreground child of the life bar
    private Vector3 lifeBarOffset = new Vector3(0, 1.5f, 0); // Offset for the life bar above the object

    protected Transform[] pathNodes;
    protected int currentNodeIndex = 0;

    void Start()
    {
        currentSpeed = speed;

        maxHealth = health;

        // Instantiate the life bar
        if (lifeBarPrefab != null)
        {
            lifeBarInstance = Instantiate(lifeBarPrefab, transform.position + lifeBarOffset, Quaternion.identity, transform);

            // Find the foreground child
            lifeBarForeground = lifeBarInstance.transform.Find("Foreground");
        }
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

        // Update the life bar position
        if (lifeBarInstance != null)
        {
            lifeBarInstance.transform.position = transform.position + lifeBarOffset;
        }

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
        UpdateLifeBar();
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateLifeBar()
    {
        if (lifeBarForeground != null)
        {
            float healthPercentage = (float)health / maxHealth;

             // Adjust only the width of the life bar foreground
            Vector3 newScale = lifeBarForeground.localScale;
            newScale.x = healthPercentage * 2; // Maintain original scale and adjust only the width
            lifeBarForeground.localScale = newScale;
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
        // Destroy the life bar
        if (lifeBarInstance != null)
        {
            Destroy(lifeBarInstance);
        }

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

        // Destroy the life bar
        if (lifeBarInstance != null)
        {
            Destroy(lifeBarInstance);
        }

        Destroy(gameObject);
    }
}
