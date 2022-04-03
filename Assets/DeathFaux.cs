using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFaux : MonoBehaviour
{
    public AudioClip attackSound;
    public PlayerDamager damager;
    public SpriteRenderer rend;
    public SpriteRenderer handRend;
    public Sprite idle, telegraph, handIdle, handAttack;
    public List<Sprite> attackSprite;
    public IEnumerator AttackRoutine(int count){
        bool fromLeft = true;
        handRend.sprite = handAttack;
        for (int i = 0; i < count; i++)
        {
            rend.flipX = !fromLeft;
            int telegraphAmount = 2;
            rend.sprite = telegraph;
            for (int j = 0; j < telegraphAmount; j++)
            {
                transform.position = new Vector3(0,0.01f,0);
                yield return new WaitForSeconds(0.1f);
                transform.position = new Vector3(0,0,0);
                yield return new WaitForSeconds(0.1f);
            }
            transform.position = new Vector3(fromLeft ? -0.02f : 0.02f,0,0);
            yield return new WaitForSeconds(0.4f);
            GM.Audio.SFX(attackSound);
            damager.TryHurt();
            foreach (Sprite s in attackSprite)
            {
                rend.sprite = s;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.2f);
            fromLeft = !fromLeft;
        }
        rend.flipX = false;
        handRend.sprite = handIdle;
        rend.sprite = idle;
    }
}
