using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public GameObject projectilePrefab; // Le prefab du projectile
    public float fireRate = 1f;         // Temps entre chaque tir
    public Transform firePoint;         // Point d'où partent les projectiles
    public Transform cannonSprite;      // Transform du sprite du canon
    public float rotationSpeed = 720f;  // Vitesse de rotation du canon (en degrés/seconde)
    
    private List<GameObject> enemiesInRange = new List<GameObject>(); // Liste des ennemis dans le rayon
    private float fireCooldown = 0f;    // Temps restant avant le prochain tir
    private Tower towerScript;
    public float projectileSpeed = 5f;  // Vitesse initiale du projectile

    void Start()
    {
        towerScript = GetComponent<Tower>();
    }

    void Update()
    {
        // Retirer les ennemis détruits de la liste
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            // Cibler le premier ennemi dans la liste
            GameObject target = enemiesInRange[0];

            if (target != null && fireCooldown <= 0f)
            {
                // Commencer l'alignement et le tir
                StartCoroutine(AimAndShoot(target));
                fireCooldown = fireRate; // Réinitialiser le cooldown
            }
        }

        // Réduire le cooldown
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    private IEnumerator AimAndShoot(GameObject target)
    {
        if (cannonSprite != null && target != null)
        {
            // Calculer la direction vers la cible
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Calculer l'angle nécessaire pour aligner le canon vers la cible
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Appliquer une rotation douce pour le canon
            while (Mathf.Abs(Mathf.DeltaAngle(cannonSprite.localEulerAngles.z, targetAngle)) > 1f)
            {
                // Effectuer une rotation douce
                float currentAngle = cannonSprite.localEulerAngles.z;
                float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);

                // Appliquer la nouvelle rotation au canon
                cannonSprite.localEulerAngles = new Vector3(0, 0, newAngle);

                yield return null; // Attendre la prochaine frame
            }

            // Une fois que le canon est aligné, tirer le projectile
            Shoot(target);
        }
    }

    private void Shoot(GameObject target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instancier le projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Définir la direction du projectile en fonction de la rotation du canon
            Vector3 cannonDirection = cannonSprite.up; // Le haut du canon correspond à sa direction
            projectile.GetComponent<Rigidbody2D>().velocity = cannonDirection * projectileSpeed;

            // Affecter d'autres propriétés au projectile
            Projectile projScript = projectile.GetComponent<Projectile>();
            projScript.SetDamage(towerScript.damage);
            if (projScript != null)
            {
                projScript.SetTarget(target);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
