using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public AudioClip hitSound;
    public virtual void Hit(){
        if(hitSound != null){
            GM.Audio.SFX(hitSound);
        }
    }
}
