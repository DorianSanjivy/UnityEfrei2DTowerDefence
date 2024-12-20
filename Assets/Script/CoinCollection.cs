using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the coin has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Increment the player's money by 1
            GlobalVariables.playerMoney += 1;

            // Optionally destroy the coin after collection
            Destroy(gameObject);
            SoundManager.Play("CoinCollected");
        }
    }
}
