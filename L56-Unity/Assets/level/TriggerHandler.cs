using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{

    public GameObject player; // Das Player-Objekt, das der Trigger betrifft
    public Rigidbody rb;
    public GameObject targetObject; // Die Zielposition, wohin der Player bewegt werden soll
    public float moveSpeed = 1.0f; // Geschwindigkeit, mit der der Player sich bewegt
    private PlayerController playerController; // Referenz auf das Player-Controller-Script
    private bool movePlayer = false;  // Schalter, um die Bewegung zu starten




    // Start is called before the first frame update
    void Start()
    {
        // Finde das PlayerController-Script
        playerController = player.GetComponent<PlayerController>();
        rb = player.GetComponent<Rigidbody>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");

        // Wenn der Player in den Trigger eintritt
        if (other.gameObject == player)
        {
            
            // Deaktiviere das PlayerController-Script, um die Steuerung zu blockieren
            playerController.enabled = false;

            rb.isKinematic = true;
            
            // Starte die Bewegung
            movePlayer = true;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Wenn die Bewegung aktiv ist
        if (movePlayer)
        {
            
            // Die Position des Ziel-GameObjects
            Vector3 targetPosition = targetObject.transform.position;

            // Bewege den Player schrittweise Richtung Ziel
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Überprüfen, ob der Player in der Nähe des Zielobjekts angekommen ist
            if (Vector3.Distance(player.transform.position, targetPosition) < 0.01f)
            {
                // Stoppe die Bewegung, wenn er das Ziel erreicht hat
                movePlayer = false;

                // Optional: Steuerung wieder aktivieren
                // playerController.enabled = true;
            }
        }
    }
}
