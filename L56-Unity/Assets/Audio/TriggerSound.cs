using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip triggerSound; // Ziehe hier den Soundclip in den Inspector
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource auf diesem GameObject holen
        audioSource = GetComponent<AudioSource>();
    }

    // Wenn der Player den Trigger betritt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Annahme: Der Player hat das Tag "Player"
        {
            // Spiele den Soundclip einmal ab
            audioSource.PlayOneShot(triggerSound);
        }
    }
}
