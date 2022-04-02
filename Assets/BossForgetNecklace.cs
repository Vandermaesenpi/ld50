using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForgetNecklace : Hittable
{
    public BossForget boss;
    public SpriteBlink blink;
    public bool invincible = true;

    public override void Hit()
    {
        if(invincible){return;}
        base.Hit();
        invincible = true;
        blink.Blink();
        boss.OnNecklaceHit();
    }
}
