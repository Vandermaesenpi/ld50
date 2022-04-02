using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForget : Boss
{
    public SpriteRenderer headRenderer;
    public BopUpDown headBop;
    public BopUpDown necklaceBop;
    public SpriteAnimator necklace;
    public SpriteAnimator shadow;
    public List<Sprite> shadowAnimation;
    public List<Sprite> appearAnimation;
    public List<Sprite> necklaceAnimation;
    public List<Sprite> headStates;

    public override void Damage(int amount)
    {
        StartCoroutine(HurtAnim(0.5f));
        base.Damage(amount);
    }


    private void Start() {
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

        StartCoroutine(AIRoutine());
    }



    IEnumerator AIRoutine(){
        while (HP > 0)
        {
            foreach (BossHand hand in hands)
            {
                hand.gameObject.SetActive(true);
                yield return null;
                if(!hand.hurt){
                    hand.UpdateState(HandState.SCANNING);
                }
            }
            while (!AllHandsHurt)
            {
                yield return null;
            }
            yield return null;

            // While Both Hands OK
            // Hand scans + punch

        // Open necklace

        // If necklace hit OR wait X sec
        }        

    }

    IEnumerator HurtAnim(float time){
        headRenderer.sprite = headStates[1];
        yield return new WaitForSeconds(time);
        headRenderer.sprite = headStates[0];
    }
}
