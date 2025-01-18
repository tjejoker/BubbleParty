using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBubble : MonoBehaviour
{
    public float lifeTime = 5f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            animator.Play("BubbleG_Destroy");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().hp += 3;
            Destroy(gameObject);
        }
    }
}
