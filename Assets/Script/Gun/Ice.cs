using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private static BufferPool<Ice> _pool;

    private static BufferPool<Ice> Pool
        => _pool ??= new BufferPool<Ice>(Resources.Load<GameObject>($"Ice"));


    private float _size;

    public float Size
    {
        get => _size;
        set
        {
            _size = value;
            transform.localScale = new Vector3(_size, _size, _size);

            if (_size < 0.1f)
            {
                Destroy();
            } 
        }
    }

    public static void Create(Vector3 position, float size)
    {
        var ice = Pool.Create();
        ice.transform.position = position;
        ice.transform.localScale = new Vector3(size, size, size);
        
        ice.Size = size;
    }


    private void Destroy()
    {
        Pool.Destroy(this);
    }
}