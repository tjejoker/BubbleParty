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
        Bubble.Create(shootPos.position, shootPos.rotation, new BubbleMoveWay(this));
        
        TimerInterval.Create(frequency, () => {IsColdDown = false; });
    }
}


public class BubbleMoveWay : BulletMoveWay
{
    private float _time;

    private readonly Vector3 _direction;
    private readonly Vector3 _normal;

    private readonly float _lifeTime;
    private readonly float _speed;
    private readonly float _amplitude;
    private readonly float _size;

    private readonly AnimationCurve _speedFactor;
    private readonly AnimationCurve _sizeFactor;
    private readonly AnimationCurve _amplitudeFactor;

    public BubbleMoveWay(BubbleGun gun)
    {
        var offsetAngle = Random.Range(gun.offsetAngleMin, gun.offsetAngleMax);
        var q = Quaternion.AngleAxis(offsetAngle, Vector3.up);
        _direction = q * gun.shootPos.transform.right;
        _normal = Vector3.Cross(_direction, Vector3.up);

        _size = Random.Range(gun.sizeMin, gun.sizeMax);
        _lifeTime = Random.Range(gun.lifeTimeMin, gun.lifeTimeMax);
        _speed = Random.Range(gun.speedMin, gun.speedMax);
        _amplitude = Random.Range(gun.amplitudeMin, gun.amplitudeMax);

        _speedFactor = gun.speedFactor;
        _sizeFactor = gun.sizeFactor;
        _amplitudeFactor = gun.amplitudeFactor;
    }


    public override void Update<T>(BulletBase<T> bullet, float dt)
    {
        // TODO: 计算运动效果， 动画效果
        var bubble = bullet as Bubble;
        
        _time += dt;
        var t = _time / _lifeTime;

        var v = _direction * (_speed * dt * _speedFactor.Evaluate(t));
        var h = _normal * (_amplitude * dt * _amplitudeFactor.Evaluate(t));
        var s = Vector3.one * (_size * _sizeFactor.Evaluate(t));

        ((bullet as Bubble)!).size = _size * _sizeFactor.Evaluate(t);
        bullet.transform.position += v + h;
        bullet.transform.localScale = s;
        
        if (_time >= _lifeTime)
        {
            bullet.Release();
        }
    }
}