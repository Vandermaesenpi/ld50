using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;

    public void SetNewState(PlayerState state){
        
    }
}

public enum PlayerState{
    IDLE,
    MOVING,
    JUMPING
}
