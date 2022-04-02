using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public BoxCollider2D col;
    public void TryHurt(){
        Debug.Log(col.OverlapPoint(GM.Player.transform.position));
        if(col.OverlapPoint(GM.Player.transform.position)){
            GM.Player.Hurt(GM.Player.transform.position - transform.position);
        }
    }
}
