using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int HP;

    public List<BossHand> hands;

    public bool AllHandsHurt { get{
        foreach (BossHand hand in hands)
        {
            if(!hand.hurt){
                return false;
            }
        }
        return true;
    }}


    public virtual void Damage(int amount){
        HP -= amount;
        if(HP <= 0){
            Die();
        }
    }

    public virtual void Die(){

    }
}
