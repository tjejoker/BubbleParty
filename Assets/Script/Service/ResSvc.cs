//
// ResSvc
// 资源加载服务

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;
    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        string path = Constant.AudioClipFolder + name;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogError($"没有找到对应音频文件，文件路径为 ： {path}");  
        }
        return clip;
    }

    public GameObject GetGameObject(string name)
    {
        string path = Constant.PrefabFolder + name;
        GameObject obj = Resources.Load<GameObject>(path);
        if (obj == null)
        {
            Debug.LogError($"没有找到对应游戏对象文件，文件路径为 ： {path}");  
        }
        return obj;
    }

    public Sprite GetSprite(string name)
    {
        string path = Constant.SpriteFolder + name;
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            Debug.LogError($"没有找到对应图片素材文件，文件路径为 ： {path}");  
        }
        return sprite;
    }
}
