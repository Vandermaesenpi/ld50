using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public SpriteAnimator deathUI;
    public List<Sprite> deathAnim;

    public GameObject deathRestartButton;

    public void ShowDeathUI(){
        foreach (GameObject boss in GM.I.bosses)
        {
            boss.SetActive(false);
        }
        deathUI.spriteRenderer.enabled = true;
        deathUI.SetAnimation(new List<Sprite>{deathAnim[deathAnim.Count-1]}, deathAnim, DeathUIShown);
    }

    public void DeathUIShown(){
        deathRestartButton.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }
}
