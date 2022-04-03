using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public SpriteAnimator deathUI;
    public List<Sprite> deathAnim;

    public GameObject deathRestartButton;
    public BossForget firstBoss;
    public GameObject mainMenu;

    public AudioClip firstMusic;
    public AudioClip deathSound;

    public void ShowDeathUI(){
        foreach (GameObject boss in GM.I.bosses)
        {
            boss.SetActive(false);
        }
        deathUI.spriteRenderer.enabled = true;
        deathUI.SetAnimation(new List<Sprite>{deathAnim[deathAnim.Count-1]}, deathAnim, DeathUIShown);
    }

    public void DeathUIShown(){
        GM.Audio.SFX(deathSound);
        deathRestartButton.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }

    public void StartGame(){
        mainMenu.SetActive(false);
        GM.Player.health.gameObject.SetActive(true);
        GM.Audio.SetMusic(firstMusic);
        firstBoss.StartGame();
    }
}
