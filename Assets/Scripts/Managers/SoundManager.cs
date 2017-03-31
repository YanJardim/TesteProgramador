using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para executar musica e efeitos sonoros
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    //Lista com todos os audios disponiveis para tocar
    public List<AudioClip> audios;
    //AudioSources para tocar sons ao mesmo tempo
    public AudioSource sfxSource, sfxSource2, musicSource;


    private void Start()
    {
        PlayMusic("music");
    }

    /// <summary>
    /// Metodo para tocar um efeito sonoro
    /// </summary>
    /// <param name="audioName"></param>
    public void PlaySfx(string audioName)
    {
        //Percorre a lista de audios
        foreach (AudioClip audio in audios)
        {
            //Verifica se o nome do audio é igual ao nome da lista
            if (audio.name == audioName)
            {
                //Verifica se o primeiro AudioSource está tocando
                if (!sfxSource.isPlaying)
                {
                    //Toca uma vez o audio encontrado
                    sfxSource.PlayOneShot(audio);
                }
                else
                {
                    //Caso o primeiro AudioSource esteja tocando o segundo toca o som encontrado
                    sfxSource2.PlayOneShot(audio);
                }
            }
        }
    }
    /// <summary>
    /// Metodo para tocar uma musica
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayMusic(string audioName)
    {
        foreach (AudioClip audio in audios)
        {
            if (audio.name == audioName)
            {
                musicSource.clip = audio;
                musicSource.Play();
            }
        }
    }
}
