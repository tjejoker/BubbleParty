using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class RockGun : GunBase
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Fire()
    {
        if (IsColdDown)
            return;

        IsColdDown = true;

        var bullet = RockBullet.Create(shootPos.position, shootPos.rotation);
        bullet.Initialize(shootPos.right, speed);

        TimerInterval.Create(frequency, () => IsColdDown = false);
        // TODO: 播放音效
        AudioManager.Instance.PlayEffect(ResSvc.Instance.GetAudioClip("飞行/飞行1"));
    }
}