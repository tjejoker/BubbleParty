using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Bubble : BulletBase<Bubble>
{
    private float _time;

    private Vector3 _direction;
    private Vector3 _normal;

    private float _lifeTime;
    private float _speed;
    private float _amplitude;
    private float _size;

    private AnimationCurve _speedFactor;
    private AnimationCurve _sizeFactor;
    private AnimationCurve _amplitudeFactor;

    public float size;

    public Animator animator;
    private float _breakAnimLength;
    public void Start()
    {
        animator.Play($"Idle");
        _breakAnimLength = 0.35f;
        // var clips = animator.runtimeAnimatorController.animationClips;
        // foreach (var clip in clips)
        // {
        //     if (clip.name != "Break") 
        //         continue;
        //     _breakAnimLength = clip.length;
        //     Debug.Log(_breakAnimLength);
        //     break;
        // }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void Initialization( BubbleGun gun)
    {
        _time = 0;
        
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
        
        GlobalUpdate.Instance.Register(this);
    }


    public override void Run(float dt)
    {
        // TODO: 计算运动效果， 动画效果
        
        _time += dt;
        var t = _time / _lifeTime;

        var v = _direction * (_speed * dt * _speedFactor.Evaluate(t));
        var h = _normal * (_amplitude * dt * _amplitudeFactor.Evaluate(t));
        var s = Vector3.one * (_size * _sizeFactor.Evaluate(t));

        size = _size * _sizeFactor.Evaluate(t);
        transform.position += v + h;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.localScale = s;

        if (_time > _lifeTime - _breakAnimLength && !animator.GetCurrentAnimatorStateInfo(0).IsName("Break"))
        {
            animator.SetTrigger($"Break");
        }
        
        if (_time >= _lifeTime)
        {
            Release();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.bubbleDeBuff += size * 0.5f;
            Release();
        }
    }
    
}