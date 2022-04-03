using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForget : Boss
{
    public BopUpDown headBop;
    public BopUpDown necklaceBop;
    public BossForgetNecklace necklaceHittable;
    public SpriteAnimator necklace;
    public SpriteAnimator shadow;
    public List<Sprite> shadowAnimation;
    public List<Sprite> appearAnimation;
    public List<Sprite> necklaceAnimation;
    public SpriteRenderer holeRenderer;
    public GameObject groundFloor;
    public GameObject nextBoss;


    public bool necklaceOpen = false;

    public void StartGame() {
        shadow.SetAnimation(shadowAnimation, OnShadowEnd);
    }

    public void OnShadowEnd(){
        necklace.SetAnimation(appearAnimation, OnAppearEnd);
    }

    public void OnAppearEnd(){
        necklaceBop.enabled = true;
        necklace.SetAnimation(new List<Sprite>{necklaceAnimation[0]});
        headRenderer.enabled = true;
        headBop.enabled = true;

        aiRoutine = StartCoroutine(AIRoutine());
        bar.gameObject.SetActive(true);
    }



    IEnumerator AIRoutine(){

        bar.SetValue(HP, 15);

        while (HP > 0)
        {
            foreach (BossHand hand in hands)
            {
                hand.gameObject.SetActive(true);
                yield return null;
                hand.UpdateState(HandState.SCANNING);
            }
            while (!AllHandsHurt)
            {
                yield return null;
            }
            yield return null;

            necklace.SetAnimation(new List<Sprite>{necklaceAnimation[necklaceAnimation.Count-1]}, necklaceAnimation,  OnNecklaceOpen);
            float necklaceWait = 5f;
            while(!necklaceOpen){
                yield return null;
            }
            necklaceHittable.invincible = false;

            while (necklaceOpen && necklaceWait > 0)
            {
                necklaceWait -= Time.deltaTime;
                yield return null;
            }
            List<Sprite> reverseNecklace = new List<Sprite>(necklaceAnimation);
            reverseNecklace.Reverse();
            necklace.SetAnimation(new List<Sprite>{necklaceAnimation[0]}, reverseNecklace);
        }        

    }

    private void OnNecklaceOpen()
    {
        necklaceOpen = true;
    }

    public void OnNecklaceHit()
    {
        necklaceOpen = false;
        Damage(3);
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
        yield return new WaitForSeconds(1f);
        headRenderer.sprite = headStates[1];
        holeRenderer.enabled = true;
        groundFloor.SetActive(false);
        yield return new WaitForSeconds(2f);
        headRenderer.sprite = headStates[1];
        GM.Player.transform.position = Vector3.up * 2f;
        groundFloor.SetActive(true);
        nextBoss.SetActive(true);
        gameObject.SetActive(false);
    }

    
}
