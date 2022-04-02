using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public CircleCollider2D col;
    public LayerMask mask;
    public void Attack(){
        List<Collider2D> hittables = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = mask;
        col.OverlapCollider(filter, hittables);
        foreach (Collider2D item in hittables)
        {
            Hittable hittable = item.GetComponent<Hittable>();
            if(hittable != null){
                hittable.Hit();
            }
        }

    }
}
