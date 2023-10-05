using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Configurable parameters
    [Header("Movement")]
    [SerializeField] float movementSpeed = 12f;
    [SerializeField] float gravity = -9.82f;
    [SerializeField] float jumpHeight = 3f;

    [Header("Ground Check")]
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    // Private variables
    float xInput;
    float zInput;
    Vector3 movement;
    Vector3 velocity;
    bool isGrounded;

    // Cached references
    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Ground();
        Move();
        Jump();
        Gravity();
    }

    void Ground()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude((transform.right * xInput) + (transform.forward * zInput), 1f);
        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
