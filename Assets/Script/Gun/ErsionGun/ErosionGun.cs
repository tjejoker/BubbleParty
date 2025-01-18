using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class ErosionGun : GunBase
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
        
        ErosionBullet.Create(shootPos.position,
            shootPos.rotation,
            new LineBulletMoveWay(shootPos.right, speed));
        
        TimerInterval.Create(frequency, () => IsColdDown = false);
    }
}