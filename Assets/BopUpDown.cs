using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BopUpDown : MonoBehaviour
{
    public float speed;
    public float amount;
    public float delay;

    float t = 0;
    bool countingUp;
    float actualAmount;

    private void Start() {
        actualAmount = amount - delay;
    }

    void Update()
    {
        t += Time.deltaTime;
        if(t > 1f/speed){
            t = 0;
            transform.position += Vector3.up * (countingUp ? 0.01f : -0.01f);
            actualAmount -= 0.01f;
            if(actualAmount <= 0f){
                countingUp = !countingUp;
                actualAmount = amount;
            }
        }
    }
}
