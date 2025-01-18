using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Erosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public static void Create(Vector3 position, float size)
    {
        var er = ActorPool<Erosion>.Instance.Create();
        er.transform.position = position;
        er.transform.localScale = new Vector3(size, size, size);
    }




}
