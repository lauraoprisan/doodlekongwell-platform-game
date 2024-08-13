using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private enum State
    {
        Idle,
        Moving,
        Jumping,
        Dashing
    }
    private State state;

    public UnityEvent<bool> DashEvent;


    [SerializeField] MovementStatsSO movementStats;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public TrailRenderer trailRenderer;

    private float horizontal;
    private float vertical;
    private float speed;
    private float jumpingPower;
    private float fallSpeed;
    private float grounderOverlapArea;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.7f;
    private float jumpBufferCounter;
    private float dashVelocity;
    private float dashingTime;
    private bool isDashing;
    private bool canDash = true;
    private float dashCooldownTimer;
    private float dashCooldownTime;
    private Vector2 dashDir;
    private float dashTimer;

    private bool isFacingRight;

    private float _time;

    void Start()
    {
        speed = movementStats.MaxSpeed;
        jumpingPower = movementStats.JumpPower;
        fallSpeed = movementStats.MaxFallSpeed;
        grounderOverlapArea = movementStats.GrounderDistance;
        jumpBufferTime = movementStats.JumpBuffer;
        dashVelocity = movementStats.DashVelocity;
        dashingTime = movementStats.DashingTime;
        dashCooldownTime = movementStats.DashCooldown;

        state = State.Idle;
    }

    void Update()
    {
        //Debug.Log(state.ToString());

        HandleMovement();
        HandleJump();
        HandleDash();


        if (!IsGrounded())
        {
            rb.velocity += Vector2.down * fallSpeed * Time.deltaTime;

        }


    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, grounderOverlapArea, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void HandleMovement()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        state = State.Moving;
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal <= 0f)
        {
            Flip();
        }
        if (horizontal * speed == 0f && IsGrounded())
        {
            state = State.Idle;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
    private void HandleJump()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)

        {
            state = State.Jumping;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
        }

        jumpBufferCounter -= Time.deltaTime;

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            coyoteTimeCounter = 0f;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && canDash && !isDashing && dashCooldownTimer <= 0f)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            rb.velocity = Vector2.zero;
            dashDir = new Vector2(horizontal, vertical).normalized;

            if (dashDir == Vector2.zero)
            {
                dashDir = new Vector2(-transform.localScale.x, 0);
            }

            #region ClampAngles
            /* Calculate direction based on input
            Vector2 dashInput = new Vector2(horizontal, vertical).normalized;
             Clamp the dash direction to allowed angles
            float angle = Vector2.SignedAngle(Vector2.right, dashInput);
            if ((Mathf.Abs(angle - 180f) >= 150f) || Mathf.Abs(angle) <= 30f) // Dash left or right
            {
                dashDir = new Vector2(Mathf.Sign(angle), 0f);
            }
            else if (angle >= 210 && angle <= 270) // Dash down
            {
                dashDir = Vector2.down;
            }
            
            
            else if (Mathf.Abs(angle) >= 75 && Mathf.Abs(angle) <= 105) 
            {
                dashDir = Vector2.up; 
            }
            else if (Mathf.Abs(angle) >= 30 && Mathf.Abs(angle) <= 75) // Dash left-up diagonal
            {
                dashDir = new Vector2(1f, 1f).normalized;
            }
            else if (Mathf.Abs(angle) >= 105 && Mathf.Abs(angle) <= 165) // Dash right-up diagonal
            {
                dashDir = new Vector2(-1f, 1f).normalized;
            }
            else if (Mathf.Abs(angle) >= 300 && Mathf.Abs(angle) <= 345) // Dash left-down diagonal
            {
                dashDir = new Vector2(1f, -1f).normalized;
            }
            else if (Mathf.Abs(angle) >= 210 && Mathf.Abs(angle) <= 255) // Dash right-down diagonal
            {
                dashDir = new Vector2(-1f, -1f).normalized;
            }*/
            #endregion


            dashTimer = dashingTime;

        }
    }

    private void HandleDash()
    {
        if (isDashing)
        {
            DashEvent.Invoke(true);

            state = State.Dashing;
            rb.velocity = dashDir * dashVelocity;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                trailRenderer.emitting = false;
                isDashing = false;
                rb.velocity = new Vector2(horizontal * speed, 0);
                dashCooldownTimer = dashCooldownTime;
                DashEvent.Invoke(false);
            }
        }

        if (IsGrounded())
        {
            canDash = true;
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }
}