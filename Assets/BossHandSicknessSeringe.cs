using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandSicknessSeringe : BossHand
{
    public AudioClip stomp;
    public float telegraphTime;
    public float downPos;
    public float downSpeed;
    public float downTime;
    public float returnTime;
    public PlayerDamager damager;

    public LayerMask targetMask;
    public float seringeLength;


    public Vector3 idlePos;

    Coroutine currentRoutine;

    

    public override void UpdateState(HandState state)
    {
        base.UpdateState(state);
        switch (state)
        {
            case HandState.SCANNING:
            hurt = false;
            if(currentRoutine != null){StopCoroutine(currentRoutine);}
                currentRoutine = StartCoroutine(AttackRoutine());
            break;

            case HandState.TELEGRAPH:
            break;

            case HandState.ATTACKING:
            break;

            case HandState.ATTACK:
                GM.Cam.Shake(0.04f, 0.02f);
                GM.Audio.SFX(stomp);
                damager.TryHurt();            
            break;
            
            case HandState.RETURNING:
            if(currentRoutine != null){StopCoroutine(currentRoutine);}
                currentRoutine = StartCoroutine(ReturnRoutine());
            break;
            
            case HandState.HURT:
            
            break;

            case HandState.IDLE:
                if(currentRoutine != null){StopCoroutine(currentRoutine);}
            break;
        }

        currentState = state;
    }


    public IEnumerator AttackRoutine(){
        float scanTime = 1f+Random.value;
        
        // Scanning
        for (float i = 0; i < scanTime; i+=Time.deltaTime)
        {
            transform.up = Vector3.Lerp(transform.up, idlePos - GM.Player.transform.position, Time.deltaTime*10f);
            yield return null;
        }

        UpdateState(HandState.TELEGRAPH);
        yield return new WaitForSeconds(telegraphTime);

        UpdateState(HandState.ATTACKING);


        Vector3 downTargetPos = (Vector3)(Physics2D.Raycast(transform.position, -transform.up, 10f,targetMask).point)+ transform.up * seringeLength;
        // GO DOWN
        for (float i = 0; i < downSpeed* Vector3.Distance(idlePos, downTargetPos); i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(idlePos, downTargetPos, i/(downSpeed * Vector3.Distance(idlePos, downTargetPos)));
            yield return null;
        }
        transform.position = downTargetPos;

        UpdateState(HandState.ATTACK);
        yield return new WaitForSeconds(downTime);
        UpdateState(HandState.RETURNING);
    }

    public IEnumerator ReturnRoutine(){
        

        for (float i = 0; i < returnTime; i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, idlePos, i/returnTime);
            yield return null;
        }

        transform.position = idlePos;
        
        UpdateState(HandState.SCANNING);
        
    }
}
