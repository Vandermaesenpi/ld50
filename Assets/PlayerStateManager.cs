using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;

    public void UpdateState(PlayerState state, bool force = false){
        if(state == currentState){return;}
        if(currentState == PlayerState.PUNCHING && !force){return;}
        switch (state)
        {
            case PlayerState.IDLE:
            GM.Player.anim.IdleAnim();
            break;
            
            case PlayerState.MOVING:
            GM.Player.anim.WalkAnim();
            break;
            
            case PlayerState.JUMPING:
            GM.Player.anim.JumpAnim();
            break;

            case PlayerState.PUNCHING:
            GM.Player.hit.Attack();
            GM.Player.anim.PunchAnim(EndPunch);
            break;
            
        }
        currentState = state;
    }

    private void EndPunch()
    {
        UpdateState(PlayerState.IDLE, true);
    }
}

public enum PlayerState{
    IDLE,
    MOVING,
    JUMPING,
    PUNCHING,
    HURT,
    DEAD
}
