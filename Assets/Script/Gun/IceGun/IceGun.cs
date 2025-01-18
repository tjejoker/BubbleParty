using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class IceGun : GunBase
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Fire()
    {
        if(IsColdDown)
            return;
        
        IsColdDown = true;
        
        var bullet = IceBullet.Create(shootPos.position, shootPos.rotation);
        bullet.Initialize(shootPos.right, speed);
        
        TimerInterval.Create(frequency, () => IsColdDown = false);
    }
}
