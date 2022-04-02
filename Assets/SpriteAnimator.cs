using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public float animationSpeed;
    public SpriteRenderer spriteRenderer;
    Coroutine animationRoutine;

    public void SetAnimation(List<Sprite> loopAnimation, System.Action onComplete){
        SetAnimation(loopAnimation, null, onComplete);
    }


    public void SetAnimation(List<Sprite> loopAnimation, List<Sprite> startAnimation = null, System.Action onComplete = null){
        if(animationRoutine != null){StopCoroutine(animationRoutine);}
        animationRoutine = StartCoroutine(AnimationRoutine(loopAnimation, startAnimation, onComplete));
    }

    IEnumerator AnimationRoutine(List<Sprite> loopAnimation, List<Sprite> startAnimation, System.Action onComplete){
        if(startAnimation != null){
            foreach (Sprite s in startAnimation)
            {
                spriteRenderer.sprite = s;
                yield return new WaitForSeconds(animationSpeed);
            }
        }

        bool loop = true;
        while (loop){
            foreach (Sprite s in loopAnimation)
            {
                spriteRenderer.sprite = s;
                yield return new WaitForSeconds(animationSpeed);
            }
            if(onComplete != null){
                onComplete.Invoke();
                loop = false;
            }
        }
    }
}
