using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErosionGun : GunBase
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Fire()
    {
        ErosionBullet.Create(shootPos.position,
            shootPos.rotation,
            new LineBulletMoveWay(shootPos.forward, speed));
    }
}
