using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementStats", menuName = "ScriptableObjects/PlayerMovementStats")]
public class MovementStatsSO : ScriptableObject
{
  
    [Header("MOVEMENT")]
    [Tooltip("The top horizontal movement speed")]
    public float MaxSpeed = 14;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float Acceleration = 120;

    [Tooltip("The pace at which the player comes to a stop")]
    public float GroundDeceleration = 60;

    [Tooltip("Deceleration in air only after stopping input mid-air")]
    public float AirDeceleration = 30;

    [Tooltip("The detection distance for grounding and roof detection"), Range(0f, 0.5f)]
    public float GrounderDistance = 0.05f;
    
    [Header("Dash")]
    [Tooltip("The top dash velocity")]
    public float DashVelocity = 14;
    [Tooltip("Amount of time dashing")]
    public float DashingTime = 0.5f;
    [Tooltip("Dash Cooldown")]
    public float DashCooldown = 0.5f;

    [Header("JUMP")]
    [Tooltip("The immediate velocity applied when jumping")]
    public float JumpPower = 36;

    [Tooltip("The maximum vertical movement speed")]
    public float MaxFallSpeed = 40;

    [Tooltip("The player's capacity to gain fall speed. a.k.a. In Air Gravity")]
    public float FallAcceleration = 110;

    [Tooltip("The time before coyote jump becomes unusable. Coyote jump allows jump to execute even after leaving a ledge")]
    public float CoyoteTime = 0.15f;

    [Tooltip("The amount of time we buffer a jump. This allows jump input before actually hitting the ground")]
    public float JumpBuffer = .2f;
}
