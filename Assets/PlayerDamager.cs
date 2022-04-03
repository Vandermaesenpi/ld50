using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public Collider2D col;
    public void TryHurt(System.Action onHit = null){
        if(col.OverlapPoint(GM.Player.transform.position)){
            GM.Player.Hurt(GM.Player.transform.position - transform.position);
            onHit?.Invoke();
        }
    }
}
