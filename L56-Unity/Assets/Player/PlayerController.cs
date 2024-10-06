using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions; // Das Input Action Asset
    private Vector2 moveInput; // Bewegungseingabe
    private Rigidbody rb; // Rigidbody für den Player

    public float moveSpeed = 5f; // Geschwindigkeit des Spielers
    public float jumpForce = 5f; // Sprungkraft

    public float maxJumpHeight = 5f; // Maximale Sprunghöhe
    private float initialPlayerY; // Speichert die Y-Position des Spielers beim Start des Sprungs

    public float maxJumpTime = 0.5f; // Maximale Dauer, die der Spieler springen kann
    public float fallMultiplier = 2.5f; // Erhöht die Fallgeschwindigkeit
    public float lowJumpMultiplier = 2f; // Für das sofortige Stoppen des Sprungs

    public float jumpHoldForce = 2f; // Zusätzliche Kraft, solange die Taste gehalten wird
    private float jumpTimeCounter; // Zählt die Zeit, wie lange der Sprung andauert
    private bool isJumping; // Um zu tracken, ob der Spieler springt

    public bool isGrounded;
   
  
    void Awake()
    {
        // Input Actions initialisieren
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // Input-Aktionen aktivieren
        playerInputActions.Enable();

        // Bewegung und Sprung-Bindings zuweisen
        playerInputActions.Player.Move.performed += OnMove;
        playerInputActions.Player.Move.canceled += OnMove;

        playerInputActions.Player.Jump.performed += OnJumpPressed; // Sprung startet
        playerInputActions.Player.Jump.canceled += OnJumpReleased; // Sprung stoppt

    }

    private void OnDisable()
    {
        // Input-Aktionen deaktivieren
        playerInputActions.Player.Move.performed -= OnMove;
        playerInputActions.Player.Move.canceled -= OnMove;

        playerInputActions.Player.Jump.performed -= OnJumpPressed;
        playerInputActions.Player.Jump.canceled -= OnJumpReleased;
        playerInputActions.Disable();
    }

    // Bewegungseingabe verarbeiten
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    // Wird aufgerufen, wenn die Sprungtaste gedrückt wird
    private void OnJumpPressed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {   // Normales Springen vom Boden
            isJumping = true;
            jumpTimeCounter = maxJumpTime;

            initialPlayerY = transform.position.y; // Speichere die Y-Position des Spielers beim Start des Sprungs

            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0); // Erste Sprungkraft
            isGrounded = false;
        }
        
    }

    // Wird aufgerufen, wenn die Sprungtaste losgelassen wird
    private void OnJumpReleased(InputAction.CallbackContext context)
    {
        isJumping = false; // Stoppt den Sprung
    }

    // Update is called once per frame
    void Update()
    {
        // Bewegung in X-Richtung
        //float moveSpeedModifier = isGrounded ? 1f : 0.5f; // Verlangsamt die Bewegung in der Luft
        rb.velocity = new Vector3(moveInput.x * moveSpeed /* moveSpeedModifier*/, rb.velocity.y, 0);

        
        // Berechne die aktuelle Sprunghöhe
        float currentJumpHeight = transform.position.y - initialPlayerY;

        // Sprunglogik (normaler Sprung und Walljump)
        if (isJumping)
        {
            if (jumpTimeCounter > 0 && currentJumpHeight < maxJumpHeight)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce + jumpHoldForce, 0);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false; // Beendet den Sprung
            }
        }

        // Schnelleres Fallen, wenn der Spieler die Taste loslässt
        if (rb.velocity.y < 0)
        {
            // Erhöhte Schwerkraft beim Fallen (Fast Fall)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !isJumping)
        {
            // Sofort aufhören zu springen (stärkerer Schwerkraftzug)
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // Überprüfen, ob der Spieler den Boden berührt
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
}
