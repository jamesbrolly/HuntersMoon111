using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // The maximum health of the player
    private int currentHealth;  // The player's current health

    public Slider healthBar;          // Reference to the Health Bar UI Slider
     public AudioClip damageSound;      // Sound effect for taking damage
    private AudioSource audioSource;   // Reference to the AudioSource component
    
    public float knockbackForce = 10f;  // Strength of the knockback effect
    private Rigidbody rb;       // Player's Rigidbody
    public GameObject gameOverScreen; // Reference to the Game Over Screen UI Panel
    public void Start()
    {
        currentHealth = maxHealth; // Initialize health

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the Player
        rb = GetComponent<Rigidbody>();       // Get the rigidbody component attached to the Player
       
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; // Set the Slider's max value
            healthBar.value = currentHealth; // Set the initial value
        }

         // Ensure Game Over screen is hidden at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    // Method to apply damage to the player
    public void TakeDamage(int damageAmount, Vector3 attackerPosition)
    {
        currentHealth -= damageAmount; // Subtract health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        Debug.Log("Player Health: " + currentHealth);

         // Play damage sound effect
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Apply knockback effect
        ApplyKnockback(attackerPosition);

        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Update the health bar UI
        }


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void ApplyKnockback(Vector3 attackerPosition)
    {
        if (rb != null)
        {
            // Calculate the knockback direction (away from the attacker)
            Vector3 knockbackDirection = (transform.position - attackerPosition).normalized;
            knockbackDirection.y = 0; // Keep knockback horizontal

            // Apply force to the Rigidbody
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }


    // Method to handle player death
    
    private void Die()
    {
        Debug.Log("Player has died!");
        Invoke("ReloadScene", 2f);

        // Show the Game Over Screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Optional: Freeze the game
        Time.timeScale = 2f;

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
