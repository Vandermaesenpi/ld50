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

    private void FixedUpdate() {
        isGrounded = CheckIfGrounded();
        Vector2 movementVector = GM.Player.input.movementDirection;

        if(isGrounded && movementVector.y > 0){
            jumpAmount = jumpStrength;
        }

        movementVector.y = jumpAmount;

        if(!isGrounded){
            jumpAmount -= Time.fixedDeltaTime * jumpDecay;
        }

        movementVector.x *= walkSpeed * Time.fixedDeltaTime;
        Debug.Log(movementVector);

        rb.velocity = movementVector;
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.18f, groundRaycastMask);
        return hit.collider != null; 
    }
}
