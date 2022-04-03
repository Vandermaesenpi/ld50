using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    public SpriteRenderer rend;

    Coroutine blinkRoutine;

    private void OnEnable() {
        rend.material = GM.I.spriteMat;
    }

    public void Blink(){
        if(blinkRoutine != null){StopCoroutine(blinkRoutine);}
        blinkRoutine = StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine(){
        for (int i = 0; i < 3; i++)
        {
            rend.material = GM.I.blinkMat;
            yield return new WaitForSeconds(0.1f);
            rend.material = GM.I.spriteMat;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
