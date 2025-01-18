//
// AudioManager
// 功能： 音频控制系统


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource effectSource;
    public List<AudioSource> effectSources;
    public static AudioManager Instance;

    public void Init()
    {
        if (Instance == null)
        {
            AudioManager.Instance = this;
        }
        
        bgmSource.loop = true;
        effectSource.loop = false;
    }


    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
        }
        bgmSource.Play();
    }

    public void PlayEffect(AudioClip clip)
    {
        foreach (var item in effectSources)
        {
            if (!item.isPlaying || item.clip == clip)
            {
                item.clip = clip;
                item.Play();
                return;
            }
        }

        AudioSource source = effectSource.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        effectSources.Add(source);
    }
    
    
}
