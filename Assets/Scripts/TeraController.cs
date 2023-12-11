using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeraController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;
    private Vector2 movementVector;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 5;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        this.movementVector = movementValue.Get<Vector2>();
        
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Store the X and Y components of the movement.
        movementX = Mathf.Clamp(movementX + movementVector.x * 0.5f, -10f, 10f);
        movementY = Mathf.Clamp(movementY + movementVector.y * 0.5f, -10f, 10f);

        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        movementX *= 0.9f;
        movementY *= 0.9f;

    }
}