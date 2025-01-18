using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Bubble : MonoBehaviour, IUpdate
{
    private static BufferPool<Bubble> _pool;

    private static BufferPool<Bubble> Pool
        => _pool ??= new BufferPool<Bubble>(Resources.Load<GameObject>($"Bubble"));
    
    private BubbleGun _gun;

    private Vector3 _direction;
    private Vector3 _normal;

    private float _lifeTime;
    private float _speed;
    private float _amplitude;
    private float _time;
    public float size = 1.0f;
    
    private bool _isDone = false;

    public static Bubble CreateBubble(BubbleGun gun)
    {
        var bubble = Pool.Create();
        bubble.Init(gun);
        return bubble;
    }


    private void Init(BubbleGun gun)
    {
        // 初始化
        _time = 0;
        _isDone = false;
        _gun = gun;


        var offsetAngle = Random.Range(_gun.offsetAngleMin, _gun.offsetAngleMax);
        var q = Quaternion.AngleAxis(offsetAngle, Vector3.up);
        _direction = q * _gun.shootPos.transform.right;
        _normal = Vector3.Cross(_direction, Vector3.up);

        size = Random.Range(_gun.sizeMin, _gun.sizeMax);
        _lifeTime = Random.Range(_gun.lifeTimeMin, _gun.lifeTimeMax);
        _speed = Random.Range(_gun.speedMin, _gun.speedMax);
        _amplitude = Random.Range(_gun.amplitudeMin, _gun.amplitudeMax);

        transform.position = _gun.shootPos.transform.position;
        transform.localScale = Vector3.one * size;
        // 统一Update 以节省性能
        GlobalUpdate.Instance.Register(this);
    }


    // Start is called before the first frame update
    void Start()
    {
    }


    public void Run(float dt)
    {
        _time += dt;

        // TODO: 计算运动效果， 动画效果
        var t = _time / _lifeTime;

        var v = _direction * (_speed * dt * _gun.speedFactor.Evaluate(t));
        var h = _normal * (_amplitude * dt * _gun.amplitudeFactor.Evaluate(t));
        var s = Vector3.one * (size * _gun.sizeFactor.Evaluate(t));
        
        transform.position += v + h;
        transform.localScale = s;
        
        if (_time >= _lifeTime)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        _isDone = true;
        Pool.Destroy(this);
    }


    public bool IsDone()
    {
        return _isDone;
    }
 
}