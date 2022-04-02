using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]

    public Rigidbody2D rb;
    
    [Header("Params")]
    public float walkSpeed;
    public float jumpStrength;
    public float jumpDecay;
    public LayerMask groundRaycastMask;

    public bool isGrounded = true;
    public float jumpAmount = 0f;
    public float lastXDirection = 0f;

    public bool CanMove => GM.Player.state.currentState != PlayerState.PUNCHING || !isGrounded;

    private void FixedUpdate() {
        isGrounded = CheckIfGrounded();
        Vector2 movementVector = GM.Player.input.movementDirection;

        if(isGrounded && movementVector.y > 0){
            jumpAmount = jumpStrength;
            GM.Player.state.UpdateState(PlayerState.JUMPING);
        }

        movementVector.y = jumpAmount;

        if(!isGrounded){
            jumpAmount -= Time.fixedDeltaTime * jumpDecay;
            GM.Player.state.UpdateState(PlayerState.JUMPING);
        }else{
            if(movementVector.x == 0){
                GM.Player.state.UpdateState(PlayerState.IDLE);
            }else{
                GM.Player.state.UpdateState(PlayerState.MOVING);
            }
        }

        if(Mathf.Sign(movementVector.x) != Mathf.Sign(lastXDirection) && Mathf.Abs(movementVector.x) > 0f){
            GM.Player.anim.SetDirection(movementVector.x);
            lastXDirection = movementVector.x;
        }

        movementVector.x *= CanMove ? walkSpeed * Time.fixedDeltaTime : 0f;

        rb.velocity = movementVector;
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.175f, groundRaycastMask);
        return hit.collider != null; 
    }
}
