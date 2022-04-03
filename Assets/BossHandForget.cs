using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandForget : BossHand
{
    public AudioClip stomp;
    public Vector2 scanRange;
    public float telegraphTime;
    public float downPos;
    public float downSpeed;
    public float downTime;
    public float returnTime;
    public PlayerDamager damager;
    public SpriteBlink blink;

    [Header("RENDER")]
    public SpriteRenderer spriteRenderer;
    public Sprite idle, telegraph, attack, damaged;
    Vector3 idlePos;

    Coroutine currentRoutine;

    private void Awake() {
        idlePos = transform.position;
        Debug.Log(idlePos + "  " + gameObject.name);
    }

    public override void Hit()
    {
        if(currentState != HandState.ATTACK){return;}
        hurt = true;
        boss.Damage(1);
        blink.Blink();
        UpdateState(HandState.RETURNING);
        base.Hit();
    }
    public override void UpdateState(HandState state)
    {
        base.UpdateState(state);
        switch (state)
        {
            case HandState.SCANNING:
            spriteRenderer.sprite = idle;
            hurt = false;
            if(currentRoutine != null){StopCoroutine(currentRoutine);}
                currentRoutine = StartCoroutine(AttackRoutine());
            break;

            case HandState.TELEGRAPH:
            spriteRenderer.sprite = telegraph;
            break;

            case HandState.ATTACKING:
            spriteRenderer.sprite = attack;
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
        }

        currentState = state;
    }


    public IEnumerator AttackRoutine(){
        Vector3 targetPos = idlePos + Vector3.left * Random.Range(scanRange.x, scanRange.y);
        float scanTime = 1f+Random.value;
        
        // Scanning
        for (float i = 0; i < scanTime; i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(idlePos, targetPos, i/scanTime);
            yield return null;
        }

        transform.position = targetPos;

        UpdateState(HandState.TELEGRAPH);
        yield return new WaitForSeconds(telegraphTime);

        UpdateState(HandState.ATTACKING);

        Vector3 downTargetPos = new Vector3(targetPos.x, downPos, targetPos.z);
        
        // GO DOWN
        for (float i = 0; i < downSpeed; i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(targetPos, downTargetPos, i/downSpeed);
            yield return null;
        }
        transform.position = downTargetPos;

        UpdateState(HandState.ATTACK);
        yield return new WaitForSeconds(downTime);
        UpdateState(HandState.RETURNING);
    }

    public IEnumerator ReturnRoutine(){
        
        if(hurt){
            spriteRenderer.sprite = damaged;
        }else{
            spriteRenderer.sprite = telegraph;
        }

        for (float i = 0; i < returnTime; i+=Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, idlePos, i/returnTime);
            yield return null;
        }

        transform.position = idlePos;
        if(hurt){
            UpdateState(HandState.HURT);
        }else{
            UpdateState(HandState.SCANNING);
        }
        
    }
}
