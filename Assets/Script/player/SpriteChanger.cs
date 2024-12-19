using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Composant SpriteRenderer attaché à l'objet
    public Sprite idleSprite; // Sprite lorsque le joueur ne bouge pas
    public Sprite[] walkSprites; // Tableau de sprites pour l'animation de marche
    public float animationSpeed; // Vitesse d'alternance des sprites

    private int currentWalkSprite = 0; // Index du sprite actuel
    private float animationTimer = 0f; // Timer pour gérer l'animation
    private Vector3 lastPosition; // Pour détecter si le personnage bouge
    private Vector3 movementDirection; // Direction du mouvement

    void Start()
    {
        // Récupère automatiquement le SpriteRenderer s'il n'est pas assigné
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Initialise la position du personnage
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calcule la direction du mouvement
        movementDirection = (transform.position - lastPosition).normalized;

        // Vérifie si le joueur est en mouvement
        if (IsMoving())
        {
            // Gère le flip horizontal en fonction de la direction
            if (movementDirection.x > 0)
            {
                spriteRenderer.flipX = true; // Mirrored when moving right
            }
            else if (movementDirection.x < 0)
            {
                spriteRenderer.flipX = false; // Normal orientation when moving left
            }

            // Gère l'animation de marche
            HandleWalkAnimation();
        }
        else
        {
            // Affiche le sprite "idle" si le joueur est immobile
            spriteRenderer.sprite = idleSprite;
        }

        // Met à jour la dernière position
        lastPosition = transform.position;
    }

    bool IsMoving()
    {
        // Vérifie si la position actuelle est différente de la dernière position
        return transform.position != lastPosition;
    }

    void HandleWalkAnimation()
    {
        // Gère le timer pour alterner les sprites
        animationTimer += Time.deltaTime;
        if (animationTimer >= animationSpeed)
        {
            // Passe au sprite suivant
            currentWalkSprite = (currentWalkSprite + 1) % walkSprites.Length;
            spriteRenderer.sprite = walkSprites[currentWalkSprite];

            // Réinitialise le timer
            animationTimer = 0f;
        }
    }
}