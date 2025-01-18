using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Rock : MonoBehaviour
{
    
    private static BufferPool<Rock> _pool;

    private static BufferPool<Rock> Pool
        => _pool ??= new BufferPool<Rock>(Resources.Load<GameObject>($"Rock"));


    private float _hp;

    public float Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            if (_hp < 0) 
                Destroy();
        }
    }
    

    public static void Create(Vector3 position, float size)
    {
        var rock = Pool.Create();
        rock.transform.position = position;
        rock.transform.localScale = new Vector3(size, size, size);
        
        rock.Hp = size;
    }

    
    private void Destroy()
    {
        Pool.Destroy(this);
    }

}
