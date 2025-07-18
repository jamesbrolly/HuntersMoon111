using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public AudioClip hitSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource dynamically if not attached
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }
   
    public void TakeDamage(float amount)
    {

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);  // Play hit sound
        }

        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Optionally delay the destruction of the enemy to allow the sound to play
        Destroy(gameObject, deathSound.length);
    }
}
