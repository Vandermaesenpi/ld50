using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public List<SpriteRenderer> lifeTokens;

    public Sprite alive, dead;

    public void UpdateHP(int amount){
        for (int i = 0; i < lifeTokens.Count; i++)
        {
            lifeTokens[i].sprite = i < amount ? alive : dead;
        }
    }
}
