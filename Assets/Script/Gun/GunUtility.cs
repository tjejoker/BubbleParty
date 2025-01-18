using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class BulletMoveWay
{
    public abstract void Update<T>(BulletBase<T> bullet, float dt) where T : BulletBase<T>;
}


public class LineBulletMoveWay : BulletMoveWay
{
    private readonly float _speed;
    private readonly Vector3 _direction;

    public LineBulletMoveWay(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    public override void Update<T>(BulletBase<T> bullet, float dt)
    {
        bullet.transform.position += _direction * (_speed * dt);
    }
}


public static class ActorPool<T> where T : MonoBehaviour
{
    private static BufferPool<T> _pool;

    public static BufferPool<T> Instance
        => _pool ??= new BufferPool<T>(Resources.Load<GameObject>(typeof(T).Name));
}

public abstract class BulletBase<T> : MonoBehaviour, IUpdate where T : BulletBase<T>
{

    public static T Create(Vector3 origin, Quaternion faceTo)
    {
        var bullet = ActorPool<T>.Instance.Create();
        bullet.transform.position = origin;
        bullet.transform.rotation = faceTo;
        return bullet;
    }

    public abstract void Run(float dt);

    public bool IsDone()
    {
        return !isActiveAndEnabled;
    }

    public void Release()
    {
        ActorPool<T>.Instance.Destroy((T)this);
    }
}