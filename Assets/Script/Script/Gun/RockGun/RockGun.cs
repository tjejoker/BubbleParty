using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGun :GunBase
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Fire()
    {
        RockBullet.Create(shootPos.position,
            shootPos.rotation,
            new LineBulletMoveWay(shootPos.forward, speed));
    }


}
