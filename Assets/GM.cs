using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
#region SINGLETON PATTERN
    public static GM _instance;
    public static GM I
    {
        get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GM>();
                
                if (_instance == null)
                {
                    GameObject container = new GameObject("Game Manager");
                    _instance = container.AddComponent<GM>();
                }
            }
        
            return _instance;
        }
    }
#endregion

    public PlayerManager player;


    public static PlayerManager Player => GM.I.player;

}
