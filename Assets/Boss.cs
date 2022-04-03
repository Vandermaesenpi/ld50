using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioClip hurtSound;
    public AudioClip dieSound;
    public int HP;
    public SpriteRenderer headRenderer;
    public List<Sprite> headStates;
    public HealthBar bar;

    public List<BossHand> hands;
    public Coroutine aiRoutine;

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
        StartCoroutine(HurtAnim(0.5f));
        HP -= amount;
        if(HP <= 0){
            Die();
        }else{
            GM.Audio.SFX(hurtSound);
        }
        bar.SetValue(HP, 15);
    }

    public virtual void Die(){
        GM.Audio.SFX(dieSound);
    }

    public IEnumerator HurtAnim(float time){
        headRenderer.sprite = headStates[1];
        yield return new WaitForSeconds(time);
        headRenderer.sprite = headStates[0];
    }
}
