using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : Hittable
{
    public bool AimAtPlayer = true;
    private Action playerHitAction;
    private Action bossHitAction;
    public Transform bossTarget;
    public PlayerDamager damager;
    public bool thrown = false;

    public float rotateSpeed;
    public float travelSpeed;

    void Update()
    {
        if(!thrown){return;}
        float rot = rotateSpeed * Time.deltaTime *(AimAtPlayer ? 1f : -1f);
        transform.Rotate(rot * Vector3.forward);

        if(AimAtPlayer){
            damager.TryHurt(OnHit);
            Vector3 direction = GM.Player.transform.position - transform.position;
            direction.Normalize();
            transform.position += travelSpeed * 2f *direction*Time.deltaTime;
        }else{
            Vector3 direction = bossTarget.position - transform.position;
            if(direction.magnitude < 0.05f){
                bossHitAction.Invoke();
                GameObject.Destroy(gameObject);
            }
            direction.Normalize();
            transform.position += travelSpeed * 2f *direction*Time.deltaTime;
        }
    
    }

    internal void Throw(Action onPlayerHit, Action onBossHit, Transform boss)
    {
        playerHitAction = onPlayerHit;
        bossHitAction = onBossHit;
        bossTarget = boss;
        thrown = true;
    }

    public override void Hit()
    {
        AimAtPlayer = false;
        base.Hit();
    }

    public void OnHit(){
        playerHitAction.Invoke();
        GameObject.Destroy(gameObject);
    }
}
