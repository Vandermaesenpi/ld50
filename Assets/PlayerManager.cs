using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int HP;
    public PlayerInputManager input;
    public PlayerMovement movement;
    public PlayerStateManager state;
    public PlayerAnimator anim;
    public PlayerHit hit;
    public PlayerHP health;
    internal void Hurt(Vector2 direction)
    {
        if(movement.knockBack <= 0){
            if(HP == 0){
                Die();
                return;
            }
            movement.KnockBack(Mathf.Sign(direction.x));
            anim.HurtAnim();
            HP--;
            health.UpdateHP(HP);
        }
    }

    public void Die(){
        anim.DieAnim();
        state.UpdateState(PlayerState.DEAD);
        movement.enabled = false;
    }
}
