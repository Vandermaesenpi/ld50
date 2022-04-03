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
    public EnnemyManager ennemy;
    public CameraManager cam;
    public UIManager ui;
    public AudioManager audioManager;

    public Material blinkMat;
    public Material spriteMat;

    public List<GameObject> bosses;


    public static PlayerManager Player => GM.I.player;
    public static EnnemyManager Ennemies => GM.I.ennemy;
    public static CameraManager Cam => GM.I.cam;
    public static UIManager UI => GM.I.ui;
    public static AudioManager Audio => GM.I.audioManager;

}
