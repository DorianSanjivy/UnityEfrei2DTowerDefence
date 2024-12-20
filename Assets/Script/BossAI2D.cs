using UnityEngine;

public class BossAI2D : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public float slowSpeed = 1f; // Vitesse lente du boss
    public float fastSpeed = 6f; // Vitesse rapide du boss
    public float chaseRange = 10f; // Distance à laquelle le boss commence à poursuivre
    public float attackRange = 1f; // Distance d'attaque
    public float attackCooldown = 1.5f; // Temps entre les attaques
    public float slowDuration = 3f; // Temps en mode lent
    public float fastDuration = 3f; // Temps en mode rapide

    private Rigidbody2D rb;
    private Vector2 movement;
    private float lastAttackTime;

    private float stateTimer; // Minuteur pour alterner entre les états
    private bool isFast = false; // Indique si le boss est en mode rapide
    private float moveSpeed; // Vitesse actuelle du boss

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = slowSpeed; // Démarre en mode lent
        stateTimer = slowDuration; // Démarre avec un timer de mode lent

        // Si le joueur n'est pas défini, trouvez-le automatiquement
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Alterne entre les modes lent et rapide
        HandleSpeedState();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            movement = Vector2.zero; // Stop moving si le joueur est hors de portée
        }
    }

    void FixedUpdate()
    {
        MoveBoss();
    }

    void MoveBoss()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            Debug.Log("Le boss attaque le joueur !");
            lastAttackTime = Time.time;

            // Ajoutez ici des animations ou dégâts au joueur
        }
    }

    private void HandleSpeedState()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            if (isFast)
            {
                // Passe en mode lent
                moveSpeed = slowSpeed;
                stateTimer = slowDuration;
            }
            else
            {
                // Passe en mode rapide
                moveSpeed = fastSpeed;
                stateTimer = fastDuration;
            }

            isFast = !isFast;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiser les portées dans l'éditeur
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}