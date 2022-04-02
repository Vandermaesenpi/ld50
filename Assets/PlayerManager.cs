using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInputManager input;
    public PlayerMovement movement;
    public PlayerStateManager state;
    public PlayerAnimator anim;
    public PlayerHit hit;
    internal void Hurt(Vector2 direction)
    {
        movement.KnockBack(Mathf.Sign(direction.x));
        anim.HurtAnim();
    }
}
