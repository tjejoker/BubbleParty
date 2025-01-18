using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Framework;
using UnityEngine;

public class BubbleGun : GunBase
{
    public float speedMin;
    public float speedMax;

    public float sizeMin;
    public float sizeMax;

    public float lifeTimeMin;
    public float lifeTimeMax;

    public float amplitudeMin;
    public float amplitudeMax;

    public float offsetAngleMin;
    public float offsetAngleMax;

    public AnimationCurve speedFactor;
    public AnimationCurve sizeFactor;
    public AnimationCurve amplitudeFactor;

    // Start is called before the first frame update
    void Start()
    {
    }


    public override void Fire()
    {
        if(IsColdDown)
            return;
        
        IsColdDown = true;
        var b = Bubble.Create(shootPos.position, shootPos.rotation);
        b.Initialization(this);
        TimerInterval.Create(frequency, () => {IsColdDown = false; });
    }
}
