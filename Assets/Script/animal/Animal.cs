using UnityEngine;

public class Animal : MonoBehaviour
{
    public float speed;
    private float currentSpeed;
    public int health;
    public int damage;
    private bool isSlowed = false;
    public int moneyDrop;

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

    public void ApplySlow(float slowFactor, float duration)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            currentSpeed *= slowFactor; // R�duit la vitesse
            Debug.Log(gameObject.name + " est ralenti � " + currentSpeed + " speed :" + speed);

            // Restaure la vitesse apr�s la dur�e
            Invoke("ResetSpeed", duration);
        }
    }

    private void ResetSpeed()
    {
        currentSpeed = speed; // R�initialise la vitesse normale
        isSlowed = false;
        Debug.Log(gameObject.name + " retrouve sa vitesse normale.");
    }

    protected virtual void OnReachEnd()
    {
        Destroy(gameObject);
        GlobalVariables.grangeCurrentHealth -= damage;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        GlobalVariables.playerMoney += moneyDrop;
    }
}
