using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;


public class FireBullet : BulletBase<FireBullet>
{
    private Vector3 _direction;
    private float _speed;

    public void Initialize(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        GlobalUpdate.Instance.Register(this);
    }

    public override void Run(float dt)
    {
        transform.position += _direction * (_speed * dt);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();

            Explosion.Create(bubble.transform.position, bubble.size);
            bubble.Release();
        }

        if (other.CompareTag("Rock"))
        {
            Release();
        }
    }
}