using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [Header("stats")]
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float jumpForce = 10f; // Adjust the jump force as neede

    [Header("states")]
    private Rigidbody2D rb;
    private Animator animator; // Fixed variable name
    private bool IsAttacking; // Fixed variable assignment
    private bool IsFacingRight = true; // Added variable to track player's facing direction
    private bool IsGrounded; // Variable to track if the player is grounded
  
  

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        IsAttacking = false;
    }

    private void Update()

    {
        // Get the horizontal input (left/right arrow keys or A/D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the velocity
        Vector2 movement = new Vector2(horizontalInput, 0f) * moveSpeed;

        // Apply the velocity
        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        // Call the AnimationController method
        AnimationController();

        // Flip the player if moving in the opposite direction
        if (horizontalInput > 0 && !IsFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && IsFacingRight)
        {
            Flip();
        }

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }

        // Check for attack input
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("aaaa");
            if (IsAttacking) { rb.velocity = new Vector2(0, 0); }
            Attack();
        }
    }


    private void AnimationController()
    {
        bool IsMoving = rb.velocity.x != 0; // Fixed variable name and reference
        animator.SetBool("IsRunning", IsMoving);
        animator.SetBool("IsAttacking", IsAttacking);
        animator.SetBool("IsJumping", !IsGrounded); // Set the jump animation parameter based on whether the player is grounded or not
    }

    private void Attack()
    {

        // Set IsAttacking to true to trigger attack animation
        IsAttacking = true;
       

    }

private void JUMP()
    {

        // Set IsAttacking to true to trigger attack animation
        animator.SetBool("IsJumping", true);
        animator.SetTrigger("IsJumping");
    }

// Method to flip the player horizontally
private void Flip()
    {
        IsFacingRight = !IsFacingRight; // Toggle the facing direction
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Invert the x scale to flip horizontally
        transform.localScale = newScale;
    }
    public void stopattacking()
    {
        IsAttacking = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        IsGrounded = false; // Player is no longer grounded after jumping
    }
}

