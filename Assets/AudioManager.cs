using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource, sfxSource;

    public void SetMusic(AudioClip music){
        musicSource.clip = music;
        musicSource.Play();
    }

    public void SFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }
}
