//
// GameRoot
// 游戏最高根节点

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameRoot : MonoBehaviour
{
    public AudioManager audioManager;
    public ResSvc resSvc;
    public static GameRoot Instance;
    public bool isPlayer;
    public bool isStart;

    public List<GameObject> playerUIList;
    public Transform playerUITransform;

    public Transform countDonw;

    public int PlayerCount;
    public int PlayerReadyCount;

    public GameObject goldBubble;
    public Transform winPanel;

    public List<PlayerUI> uiDic;
    


    private void Awake()
    {
        Instance = this;
        audioManager = GetComponent<AudioManager>();    
        audioManager.Init();
        resSvc = GetComponent<ResSvc>();
        resSvc.Init();
        uiDic = new List<PlayerUI>();
    }


    private void Start()
    {
        AudioManager.Instance.PlayBGM(ResSvc.Instance.GetAudioClip("BGM/bgm1"));
    }

    private void Update()
    {
        AddPlayer();
        ChekcReady();
        GerenateGoldBubble();
        CheckWin();
    }

    float lastTime;
    public float maxTime = 3;
    public void GerenateGoldBubble()
    {
        if (isStart)
        {
            lastTime += Time.deltaTime;
            if (lastTime > maxTime)
            {
                float posX = UnityEngine.Random.Range(-43, 44);
                float posY = UnityEngine.Random.Range(-30, 12);
                GameObject go = Instantiate(goldBubble);
                go.transform.position = new Vector2(posX, posY);
                go.transform.SetParent(transform);
                lastTime -= maxTime;
            }
        }
        
    }

    void CheckWin()
    {
        if (isStart && PlayerCount == 1)
        {
            //TODO win
            winPanel.gameObject.SetActive(true);
            winPanel.GetChild(0).GetComponent<Image>().sprite = uiDic[0].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        }
    }

    void ChekcReady()
    {
        if (isStart)
        {
            return;
        }
        if (PlayerCount < 1 ||PlayerCount != PlayerReadyCount)
        {
            return;
        }
        countDonw.gameObject.SetActive(true);
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
            player.playerCtrl = PlayerCtrl.Player1;
            AddPlayerUI(player);
            hasPlayerOne = true;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) && !hasPlayerTwo)
        {
            //TODO 加入玩家二
            GameObject go = GetPlayer();
            Player player = go.GetComponent<Player>();
            player.playerCtrl = PlayerCtrl.Player2;
            AddPlayerUI(player);
            hasPlayerTwo = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !hasPlayerThree)
        {
            //TODO 加入玩家三
            GameObject go = GetPlayer();
            Player player = go.GetComponent<Player>();
            player.playerCtrl = PlayerCtrl.Player3;
            AddPlayerUI(player);
            hasPlayerThree = true;
        }
    }
    

    public GameObject GetPlayer()
    {
        GameObject go = resSvc.GetGameObject(Constant.PlayerPath);
        GameObject player = Instantiate(go);
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = resSvc.GetSprite(Constant.PlayerImgPath +
            (count + 1));
        player.transform.position = Vector3.zero;
        return player;
    }

    private int count = 0;
    public void AddPlayerUI(Player player)
    {
        GameObject playerUI= Instantiate(playerUIList[count]);
        playerUI.transform.SetParent(playerUITransform);
        playerUI.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite =
            resSvc.GetSprite(Constant.PlayerHeadImgPath + (count + 1));
        player.playerUI = playerUI.GetComponent<PlayerUI>();
        count++;
        PlayerCount++;
        PlayerUI ui = playerUI.GetComponent<PlayerUI>();
        uiDic.Add(ui);
    }
    
    
    public void SetStart()
    {
        isStart = true;
    }

}
