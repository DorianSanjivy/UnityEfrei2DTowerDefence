using UnityEngine;

public class AnimalAnimation : MonoBehaviour
{
    public Sprite[] walkSprites;      // Array to hold animation frames
    public float animationSpeed = 0.1f; // Time between frames
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentFrame = 0;
        timer = 0f;
    }

    void Update()
    {
        AnimateSprite();
    }

    void AnimateSprite()
    {
        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            // Move to the next frame
            currentFrame = (currentFrame + 1) % walkSprites.Length;

            // Set the current sprite
            spriteRenderer.sprite = walkSprites[currentFrame];

            // Reset the timer
            timer = 0f;

            spriteRenderer.enabled = true;
        }
    }
}
