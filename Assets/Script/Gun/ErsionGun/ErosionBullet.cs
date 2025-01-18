using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErosionBullet : BulletBase<ErosionBullet>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();
            Erosion.Create(bubble.transform.position, bubble.size);
            bubble.Release();
        }
    }
}
