using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandSablier : Hittable
{
    public Boss boss;
    public SpriteBlink blink;

    public override void Hit()
    {
        boss.Damage(1);
        blink.Blink();
        base.Hit();
    }
}
