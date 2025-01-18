using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public Transform shootPos;
    public float frequency;

    protected float _gap;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected abstract void Fire();
}
