using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public float shakeFrequency;

    Vector3 startPos;
    Coroutine currentShake;


    public float TimeD, StrengthD;

    [ContextMenu("Shake")]
    public void ShakeDebug(){
        Shake(TimeD, StrengthD);
    }

    private void Start() {
        startPos = transform.position;
    }

    public void Shake(float time, float strength){
        if(currentShake != null){StopCoroutine(currentShake);}
        currentShake = StartCoroutine(ShakeRoutine(time, strength));
    }

    IEnumerator ShakeRoutine(float time, float strength){
        for (float i = 0; i < time; i += 1f/shakeFrequency)
        {
            transform.position = startPos + (Vector3)(Random.insideUnitCircle.normalized * strength);
            yield return new WaitForSeconds(1f/shakeFrequency);
        }
        transform.position = startPos;
    }
}
