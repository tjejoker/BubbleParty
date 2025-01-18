using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Erosion : MonoBehaviour
{
    
        
    private static BufferPool<Erosion> _pool;
    private static BufferPool<Erosion> Pool
        => _pool ??= new BufferPool<Erosion>(Resources.Load<GameObject>($"Erosion"));

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public static void Create(Vector3 position, float size)
    {
        var er = Pool.Create();
        er.transform.position = position;
        er.transform.localScale = new Vector3(size, size, size);
    }




}
