using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 movementDirection;

    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        if(Input.GetButtonDown("Fire1")){
            GM.Player.state.UpdateState(PlayerState.PUNCHING);
        }
        movementDirection = new Vector2(xMovement, yMovement);
    }
}
