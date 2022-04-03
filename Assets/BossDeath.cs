using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : Boss
{
    public AudioClip winMusic;
    public SpriteBlink blink;
    public List<Transform> skullSpawns;
    public List<SmallSkull> skulls;
    public GameObject skullprefab;
    public GameObject handUp;
    public GameObject handDown;
    public GameObject restartButton;
    public DeathFaux faux;

    public SpriteRenderer bkg, head, handLeft, handRight, fauxRend, sablier, credits;

    public Sprite headIdle;
    public Sprite creditB;

    public SpriteAnimator winAnim;
    public List<Sprite> winSprites;


    public int PhaseCount {get{
        if(HP < 5){
            return 3;
        }else if(HP < 10){
            return 2;
        }else{
            return 1;
        }
    }}

    private void Awake() {
        StartCoroutine(AppearRoutine());
    }
    IEnumerator AppearRoutine(){
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            bkg.color = new Color(1,1,1,i);
            yield return 0;
        }
        bkg.color = Color.white;

        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            handLeft.color = new Color(1,1,1,i);
            handRight.color = new Color(1,1,1,i);
            fauxRend.color = new Color(1,1,1,i);
            sablier.color = new Color(1,1,1,i);
            head.color = new Color(1,1,1,i);
            yield return 0;
        }
        handLeft.color = Color.white;
        handRight.color = Color.white;
        fauxRend.color = Color.white;
        sablier.color = Color.white;
        head.color = Color.white;
        head.sprite = headIdle;
        aiRoutine = StartCoroutine(AIRoutine());
        bar.gameObject.SetActive(true);
    }

    IEnumerator AIRoutine(){

        bar.SetValue(HP, 15);

        while (HP > 0)
        {
            
            yield return new WaitForSeconds(1);
            // Phase 1 : small skulls
            
            yield return SpawnSkulls(PhaseCount*2);
            while (skulls.Count > 0)
            {
                yield return 0;
            }
            // Phase 2 : sythe hit
            yield return faux.AttackRoutine(PhaseCount);
            yield return new WaitForSeconds(1);

            // Phase 3 : sablier down
            handUp.SetActive(false);
            handDown.SetActive(true);
            yield return new WaitForSeconds(2);
            handUp.SetActive(true);
            handDown.SetActive(false);

        }        

    }

    public override void Damage(int amount)
    {
        GM.Cam.Shake(0.08f, 0.01f);
        blink.Blink();
        base.Damage(amount);
    }

    public IEnumerator SpawnSkulls(int amount){
        List<Transform> spawnPoints = new List<Transform>(skullSpawns);
        for (int i = 0; i < amount; i++)
        {
            Transform selectedPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            spawnPoints.Remove(selectedPoint);
            SmallSkull newSkull = GameObject.Instantiate(skullprefab, selectedPoint).GetComponent<SmallSkull>(); 
            skulls.Add(newSkull);
            newSkull.boss = this;
            yield return new WaitForSeconds(0.5f);
        }

    }

    public void RemoveSkull(SmallSkull smallSkull)
    {
        skulls.Remove(smallSkull);
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine(){
        GM.Cam.Shake(2f,0.02f);
        GM.Audio.SetMusic(null);
        StopCoroutine(aiRoutine);
        bar.gameObject.SetActive(false);
        GM.Player.state.UpdateState(PlayerState.DEAD);
        GM.Player.transform.position = new Vector3(0,-0.68f,0);
        GM.Audio.SFX(dieSound);
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            handUp.SetActive(true);
            handDown.SetActive(false);
            head.sprite = headStates[1];
            handLeft.color = new Color(1,1,1,1f-i);
            handRight.color = new Color(1,1,1,1f-i);
            fauxRend.color = new Color(1,1,1,1f-i);
            sablier.color = new Color(1,1,1,1f-i);
            head.color = new Color(1,1,1,1f-i);
            yield return 0;
        }
        handLeft.color = new Color(1,1,1,0);
        handRight.color = new Color(1,1,1,0);
        fauxRend.color = new Color(1,1,1,0);
        sablier.color = new Color(1,1,1,0);
        head.color = new Color(1,1,1,0);
        GM.Player.health.gameObject.SetActive(false);
        for (float i = 0; i < 1f; i+= Time.deltaTime)
        {
            bkg.color = new Color(1,1,1,1f-i);
            yield return 0;
        }
        bkg.color = new Color(1,1,1,0);
        GM.Audio.SetMusic(winMusic);
        winAnim.SetAnimation(new List<Sprite>{winSprites[winSprites.Count-1]}, winSprites);
        yield return new WaitForSeconds(2f);
        GM.Player.anim.WinAnim();
        GM.Player.movement.enabled = false;

        for (float i = 0; i < 1f; i+= Time.deltaTime*0.2f)
        {
            GM.Player.transform.position = Vector3.Lerp(new Vector3(0,-0.68f,0), new Vector3(0,1.25f,0), i);
            yield return 0;
        }
        winSprites.Reverse();
        winAnim.SetAnimation(new List<Sprite>{winSprites[winSprites.Count-1]}, winSprites);
        GM.Player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        for (float i = 0; i < 1f; i+= Time.deltaTime*0.6f)
        {
            credits.color = new Color(1,1,1,i);
            yield return 0;
        }
        credits.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(1f);
        for (float i = 0; i < 1f; i+= Time.deltaTime*0.6f)
        {
            credits.color = new Color(1,1,1,1f-i);
            yield return 0;
        }
        credits.color = new Color(1,1,1,0);
        credits.sprite = creditB;
        for (float i = 0; i < 1f; i+= Time.deltaTime*0.6f)
        {
            credits.color = new Color(1,1,1,i);
            yield return 0;
        }
        yield return new WaitForSeconds(1f);
        restartButton.SetActive(true);

    }
}
