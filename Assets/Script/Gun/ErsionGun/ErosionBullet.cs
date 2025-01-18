using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class ErosionBullet : BulletBase<ErosionBullet>
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
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();
            Erosion.Create(bubble.transform.position, bubble.size);
            bubble.Release();
        }

        if (other.CompareTag("Rock"))
        {
            Release();
        }
    }

}
