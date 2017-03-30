using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public List<AudioClip> audios;

    public AudioSource sfxSource, sfxSource2, musicSource;


    private void Start()
    {
        PlayMusic("music");
    }

    public void PlaySfx(string audioName)
    {
        foreach (AudioClip audio in audios)
        {
            if (audio.name == audioName)
            {
                if (!sfxSource.isPlaying)
                    sfxSource.PlayOneShot(audio);
                else
                {
                    sfxSource2.PlayOneShot(audio);
                }
            }
        }
    }

    public void PlayMusic(string audioName)
    {
        foreach (AudioClip audio in audios)
        {
            if (audio.name == audioName)
            {

                musicSource.PlayOneShot(audio);

            }
        }
    }
}
