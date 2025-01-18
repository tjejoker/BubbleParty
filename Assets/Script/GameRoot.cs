//
// GameRoot
// 游戏最高根节点

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public AudioManager audioManager;
    public ResSvc resSvc;
    public GameRoot Instance;
    public bool isPlayer;

    public List<GameObject> playerUIList;
    public Transform playerUITransform;

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

    private void Update()
    {
        AddPlayer();
    }


    bool hasPlayerOne = false;
    bool hasPlayerTwo = false;
    bool hasPlayerThree = false;
    bool hasPlayerFour = false;
    public void AddPlayer()
    {
        if (isPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.K)&& !hasPlayerOne)
        {
            //TODO 加入玩家一
            GameObject go = GetPlayer();
            Player player = go.GetComponent<Player>();
            AddPlayerUI(player);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !hasPlayerTwo)
        {
            //TODO 加入玩家二
            GameObject go = GetPlayer();
            Player player = go.GetComponent<Player>();
            player.playerCtrl = PlayerCtrl.Player2;
            AddPlayerUI(player);
        }
        
        
    }

    public GameObject GetPlayer()
    {
        GameObject player = resSvc.GetGameObject(Constant.PlayerPath);
        return player;
    }

    private int count = 0;
    public void AddPlayerUI(Player player)
    {
        GameObject playerUI = Instantiate(playerUIList[count]);
        Instantiate(playerUI, playerUITransform);
        player.playerUI = playerUI.GetComponent<PlayerUI>();
        count++;
    }
}
