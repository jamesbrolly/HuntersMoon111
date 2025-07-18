using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
     public float range = 50f;
    public GameObject impactEffect;
    public AudioSource audioSource;
    public AudioClip shootSound;

     void Start()
    {
        // Ensure the audio source is attached
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);  // Play shooting sound
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // Optionally add damage handling if the hit object has an enemy script
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }

            // Instantiate an impact effect
           // if (impactEffect != null)
           // {
                //Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }
    }
}
