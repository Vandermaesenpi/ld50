using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : SpriteAnimator
{

    public SpriteBlink blink;
    public List<Sprite> idle;
    public List<Sprite> walkCycle;
    public List<Sprite> jumpStart;
    public List<Sprite> jumpLoop;
    public List<Sprite> punch;
    public List<Sprite> hurt;
    public List<Sprite> die;
    public List<Sprite> win;

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

    public void HurtAnim(){
        SetAnimation(hurt);
        blink.Blink();
    }

    public void DieAnim(){
        SetAnimation(new List<Sprite>{die[die.Count-1]}, die, DeathEnd);
    }

    private void DeathEnd()
    {
        GM.UI.ShowDeathUI();
    }

    internal void WinAnim()
    {
        SetAnimation(win);
    }
}
