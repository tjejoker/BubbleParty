using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;


public class FireBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }

        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();
            Explosion.Create(bubble.transform.position, bubble.size);

            // Ice.Create(bubble.transform.position, bubble.size);
            
            // Rock.Create(bubble.transform.position, bubble.size);
            
            //Erosion.Create(bubble.transform.position, bubble.size);

            bubble.Destroy();
            
            
        }


    }
}