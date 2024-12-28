using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_zone : MonoBehaviour
{
    public float speed = 5f;             // Projectile speed
    private float damage;                  // Damage inflicted
    public float explosionRadius = 2f;   // Explosion radius
    public GameObject explosionEffect;   // Optional visual effect for explosion

    private SpriteRenderer spriteRenderer;  // Reference to the projectile's SpriteRenderer
    public Sprite[] explosionSprites;      // Array of explosion sprites
    public float explosionFrameRate = 0.1f; // Frame rate for explosion animation

    private GameObject target;           // The target
    private Vector3 startPos;            // Starting position
    private Vector3 targetPos;           // Target position
    private float startTime;             // Start time of projectile launch
    private float travelTime;            // Total flight time

    public float curveHeight = 2f;       // Height of the curve's apex
    private bool isExploding = false;    // Indicates if explosion is in progress

    private float lifetime = 10f;        // Projectile lifetime
    private float timeAlive = 0f;        // Time since launch

    public void SetDamage(float newDamage) { damage = newDamage; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget == null || !IsTargetAlive(newTarget))
        {
            if (target != null) // Use the last known target position if available
            {
                targetPos = target.transform.position;
            }
            else // Otherwise, set a default forward position
            {
                Destroy(gameObject);
            }

            startPos = transform.position;
            travelTime = Vector3.Distance(startPos, targetPos) / speed;
            startTime = Time.time;
            target = null; // Mark target as invalid
            return;
        }

        target = newTarget;
        targetPos = target.transform.position;
        startPos = transform.position;

        // Calculate travel time based on distance and speed
        travelTime = Vector3.Distance(startPos, targetPos) / speed;
        startTime = Time.time; // Record when the projectile is launched
    }


    private bool IsTargetAlive(GameObject obj)
    {
        Animal animal = obj.GetComponent<Animal>();
        return animal != null && animal.IsAlive();
    }

    void Update()
    {
        if (isExploding) return;

        // Increment time elapsed
        timeAlive += Time.deltaTime;

        // Destroy projectile if it exceeds lifetime
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        float timeElapsed = Time.time - startTime;
        float percentageComplete = timeElapsed / travelTime;

        if (travelTime < 0.1f){
            Destroy(gameObject);
        }

        if (percentageComplete < 1)
        {
            // Calculate curved position
            Vector3 midPoint = (startPos + targetPos) / 2;
            midPoint.y += curveHeight;
            transform.position = CalculateParabolicCurve(startPos, midPoint, targetPos, percentageComplete);
        }
        else
        {
            Explode();
        }
    }

    private Vector3 CalculateParabolicCurve(Vector3 start, Vector3 mid, Vector3 end, float t)
    {
        float u = 1 - t;
        return u * u * start + 2 * u * t * mid + t * t * end;
    }

    private void Explode()
    {
        isExploding = true;
        ApplyExplosionDamage();
        StartCoroutine(PlayExplosionAnimation());
    }

    private IEnumerator PlayExplosionAnimation()
    {
        for (int i = 0; i < explosionSprites.Length; i++)
        {
            spriteRenderer.sprite = explosionSprites[i];
            yield return new WaitForSeconds(explosionFrameRate);
        }

        Destroy(gameObject); // Destroy the projectile after animation
    }

    private void ApplyExplosionDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Animal enemyHealth = collider.GetComponent<Animal>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
