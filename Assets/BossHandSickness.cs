using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandSickness : BossHand
{

    public SpriteRenderer handRenderer;
    public Sprite handGrip, handThrow;
    public float telegraphTime;

    public Transform pillSpawn;
    public GameObject pillPrefab;

    public Pill currentPill;

    Coroutine currentRoutine;


    public override void UpdateState(HandState state)
    {
        base.UpdateState(state);
        switch (state)
        {
            case HandState.SCANNING:
            hurt = false;
            if(currentRoutine != null){StopCoroutine(currentRoutine);}
            handRenderer.sprite = handGrip;
            SpawnPill();
                currentRoutine = StartCoroutine(AttackRoutine());
            break;

            case HandState.TELEGRAPH:
            break;

            case HandState.ATTACKING:
            handRenderer.sprite = handThrow;
            ThrowPill();
            break;

            case HandState.ATTACK:
            break;
            
            case HandState.RETURNING:

            break;
            
            case HandState.HURT:
            
            break;
            case HandState.IDLE:
                if(currentRoutine != null){StopCoroutine(currentRoutine);}
            break;
        }

        currentState = state;
    }
    private void SpawnPill()
    {
        currentPill = GameObject.Instantiate(pillPrefab, pillSpawn).GetComponent<Pill>();
    }

    private void ThrowPill()
    {
        if(currentState == HandState.IDLE){return;}
        currentPill.Throw(PillHitPlayer, PillHitBoss, boss.transform);
    }


    public void PillHitPlayer(){
        UpdateState(HandState.SCANNING);
    }

    public void PillHitBoss(){
        boss.Damage(3);
        if(currentState != HandState.IDLE){
            UpdateState(HandState.SCANNING);
        }
    }

    public IEnumerator AttackRoutine(){
        
        yield return new WaitForSeconds(telegraphTime);
        if(currentState != HandState.IDLE){

            UpdateState(HandState.ATTACKING);
        }
    }

}
