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

    public float knockBack = 0f;
    public float knockDirection = 0f;
    public float knockForce;
    public float knockTime;

    public bool CanMove => (GM.Player.state.currentState != PlayerState.PUNCHING || !isGrounded) && knockBack <= 0f && GM.Player.state.currentState != PlayerState.DEAD;

    public void KnockBack(float direction){
        knockDirection = direction;
        knockBack = knockTime;
        GM.Player.state.UpdateState(PlayerState.HURT, true);
        rb.AddForce(new Vector2(knockDirection * knockForce, knockForce*2f), ForceMode2D.Impulse);
    }

    private void FixedUpdate() {
        isGrounded = CheckIfGrounded();
        Vector2 movementVector = GM.Player.input.movementDirection;
        if(knockBack > 0f){
            knockBack -= Time.deltaTime;
            return;
        }
        if(isGrounded && movementVector.y > 0 && knockBack <= 0f && GM.Player.state.currentState != PlayerState.DEAD){
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
