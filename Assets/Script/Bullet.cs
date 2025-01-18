using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;


public class Bullet : MonoBehaviour, IUpdate
{
    private static BufferPool<Bullet> _pool;

    private static BufferPool<Bullet> Pool
        => _pool ??= new BufferPool<Bullet>(Resources.Load<GameObject>($"Bullet"));

    public float lifeTime = 2f;
    private float _time;
    private Vector3 _direction;
    private float _speed;

    public Action OnTriggerPlayer;
    private Action<Bubble> _onTriggerBubble;

    public static Bullet Create(GunBase gun, Action<Bubble> bubble, Action player)
    {
        var bullet = Pool.Create();

        bullet._onTriggerBubble = bubble;
        bullet.OnTriggerPlayer = player;

        bullet.transform.position = gun.shootPos.position;
        bullet.transform.rotation = gun.shootPos.rotation;
        bullet._direction = gun.shootPos.right;
        bullet._speed = ((FireLineGun)gun).speed;

        bullet._time = 0;

        GlobalUpdate.Instance.Register(bullet);

        return bullet;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }

        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();

            _onTriggerBubble?.Invoke(bubble);
            // Explosion.Create(bubble.transform.position, bubble.size);
            // Ice.Create(bubble.transform.position, bubble.size);
            // Rock.Create(bubble.transform.position, bubble.size);
            //Erosion.Create(bubble.transform.position, bubble.size);
            bubble.Destroy();
        }

        if (other.CompareTag("Rock"))
        {
            Destroy();
        }
    }

    public void Run(float dt)
    {
        transform.position += _direction * (_speed * dt);
        _time += dt;
        if (_time >= lifeTime)
            Destroy();
    }

    public bool IsDone()
    {
        return !isActiveAndEnabled;
    }

    private void Destroy()
    {
        Pool.Destroy(this);
    }
}