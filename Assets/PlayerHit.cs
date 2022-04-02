using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public CircleCollider2D col;
    public void Attack(){
        foreach (Hittable item in GM.Ennemies.hittables)
        {
            if(col.OverlapPoint(item.transform.position)){
                item.Hit();
            }
        }
    }
}
