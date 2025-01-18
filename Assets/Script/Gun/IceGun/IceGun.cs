using System;
using System.Collections;
using System.Collections.Generic;
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
        IceBullet.Create(shootPos.position,
            shootPos.rotation,
            new LineBulletMoveWay(shootPos.forward, speed));
    }
}
