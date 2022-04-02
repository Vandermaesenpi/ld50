using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : Hittable
{
    public Boss boss;
    public bool hurt;

    public HandState currentState;

    public virtual void UpdateState(HandState state){
        if(state == currentState){return;}
    }

}

public enum HandState{
    SCANNING,
    TELEGRAPH,
    ATTACKING,
    ATTACK,
    RETURNING,
    HURT,
    IDLE
}
