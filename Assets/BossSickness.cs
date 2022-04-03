using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSickness : Boss
{
    public SpriteBlink blink;
    public BossHandSicknessSeringe seringe;
    public Transform bossBody;
    public GameObject nextBoss;
    public SpriteRenderer lineBeep, bkg;
   
 private void Awake() {
        StartCoroutine(AppearRoutine());
    }
    IEnumerator AppearRoutine(){
        yield return new WaitForSeconds(1f);
        for (float i = 0; i < 1f; i+= Time.deltaTime * 0.3f)
        {
            bossBody.transform.position = Vector3.Lerp(new Vector3(0,1.25f,0),Vector3.zero, i);
            yield return 0;
        }
        bossBody.transform.position = Vector3.zero;

        seringe.idlePos = seringe.transform.position;
        aiRoutine = StartCoroutine(AIRoutine());
        bar.gameObject.SetActive(true);
    }
    IEnumerator AIRoutine(){
        
        bar.SetValue(HP, 15);

        foreach (BossHand hand in hands)
        {
            hand.gameObject.SetActive(true);
            yield return null;
            hand.UpdateState(HandState.SCANNING);
        }

    }

    public override void Damage(int amount)
    {
        GM.Cam.Shake(0.08f, 0.01f);
        blink.Blink();
        base.Damage(amount);
    }

    public override void Die(){
        base.Die();
        StopCoroutine(aiRoutine);
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine(){
        bar.gameObject.SetActive(false);
        headRenderer.sprite = headStates[1];
        GM.Cam.Shake(2f,0.02f);
        foreach (BossHand hand in hands)
        {
            hand.UpdateState(HandState.IDLE);
        }
        for (float i = 0; i < 1f; i+= Time.deltaTime*0.3f)
        {
            lineBeep.color = new Color(1,1,1,i);
            yield return 0;
        }
        lineBeep.color = Color.white;
        bossBody.gameObject.SetActive(false);
        headRenderer.sprite = headStates[1];
        for (float i = 0; i < 1f; i+= Time.deltaTime*0.5f)
        {
            lineBeep.transform.position = Vector3.Lerp(new Vector3(-1.59f, 0,0), new Vector3(1.59f, 0,0), i);
            yield return 0;
        }
        nextBoss.SetActive(true);
        bkg.enabled = false;
        for (float i = 0; i < 1f; i+= Time.deltaTime*0.5f)
        {
            lineBeep.color = new Color(1,1,1,1f-i);
            yield return 0;
        }
        lineBeep.color = new Color(1,1,1,0);
        headRenderer.sprite = headStates[1];
        gameObject.SetActive(false);
    }
}
