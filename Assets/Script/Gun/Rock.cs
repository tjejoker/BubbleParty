using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Rock : MonoBehaviour
{
    
    private static BufferPool<Rock> _pool;

    private static BufferPool<Rock> Pool
        => _pool ??= new BufferPool<Rock>(Resources.Load<GameObject>($"Rock"));


    public static void Create(Vector3 position, float size)
    {
        var rock = Pool.Create();
        rock.transform.position = position;
        rock.transform.localScale = new Vector3(size, size, size);
    }

    
    

}
