using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float size;
    public SpriteRenderer filler;
    public float speed;
    
    float TargetValue = 0;

    public void SetValue(float value, float max){
        TargetValue = value * (size/max); 
    }

    void Update()
    {
        filler.size = new Vector2(Mathf.Lerp(filler.size.x, TargetValue, Time.deltaTime * speed), filler.size.y);
    }
}
