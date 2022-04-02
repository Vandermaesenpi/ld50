﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : SpriteAnimator
{
    public List<Sprite> idle;
    public List<Sprite> walkCycle;
    public List<Sprite> jumpStart;
    public List<Sprite> jumpLoop;
    public List<Sprite> punch;

    public void IdleAnim(){
        SetAnimation(idle);
    }

    public void WalkAnim(){
        SetAnimation(walkCycle);
    }

    public void JumpAnim(){
        SetAnimation(jumpLoop, jumpStart);
    }

    public void SetDirection(float xDirection){
        spriteRenderer.flipX = xDirection < 0;
    }

    internal void PunchAnim(Action onComplete)
    {
        SetAnimation(punch, onComplete);
    }
}