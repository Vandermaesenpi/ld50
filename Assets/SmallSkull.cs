using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSkull : Hittable
{
    public PlayerDamager damager;
    public float travelSpeed;
    public bool dead = false;

    public SpriteAnimator anim;
    public List<Sprite> idleSprites;
    public List<Sprite> deathSprites;

    public BossDeath boss;


    private void Start() {
        anim.SetAnimation(idleSprites);
    }

    private void Update() {
        if(dead){return;}
        damager.TryHurt(OnHit);
        Vector3 direction = GM.Player.transform.position - transform.position;
        anim.spriteRenderer.flipX = direction.x < 0;
        direction.Normalize();
        transform.position += travelSpeed * 2f *direction*Time.deltaTime;
    }

    private void OnHit()
    {
        dead = true;
        boss.RemoveSkull(this);
        Die();
    }

    private void Die()
    {
        GameObject.Destroy(gameObject);
    }

    public override void Hit()
    {
        boss.RemoveSkull(this);
        dead = true;
        anim.animationSpeed = 0.05f; 
        anim.SetAnimation(new List<Sprite>(), deathSprites, Die);
        base.Hit();
    }
}
