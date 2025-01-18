using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Erosion : MonoBehaviour
{
    public float duration = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public static void Create(Vector3 position, float size)
    {
        var er = ActorPool<Erosion>.Instance.Create();
        er.transform.position = new Vector3(position.x, position.y, 0);
        er.transform.localScale = new Vector3(size, size, size);
        
        TimerRun.Create(er.duration, t =>
        {
            // TODO 持续减小
            
        }).Complete(() =>
        {
            ActorPool<Erosion>.Instance.Destroy(er);
        });
        // TODO: 播放音效
        AudioManager.Instance.PlayEffect(ResSvc.Instance.GetAudioClip("毒气/毒气"));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.hp -=   Time.deltaTime;
        }
    }

}
