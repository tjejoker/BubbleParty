//
// GameRoot
// 游戏最高根节点

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public AudioManager audioManager;
    public ResSvc resSvc;
    public GameRoot Instance;

    private void Awake()
    {
        Instance = this;
        audioManager = GetComponent<AudioManager>();    
        audioManager.Init();
        resSvc = GetComponent<ResSvc>();
        resSvc.Init();
    }


    private void Start()
    {
        AudioManager.Instance.PlayBGM(ResSvc.Instance.GetAudioClip("BGM/bgm1"));
    }
}
