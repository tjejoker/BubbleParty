using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLineGun : GunBase
{
    public float speed;

    private void OnEnable()
    {
        _gap = frequency; 
    }

    // Update is called once per frame
    void Update()
    {
        
        _gap += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1) && _gap > frequency)
        {
            Bullet.Create(this, b =>
            {
                Explosion.Create(b.transform.position, b.size);
            }, null);
            _gap = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && _gap > frequency)
        {
            Bullet.Create(this, b =>
            {
                Ice.Create(b.transform.position, b.size);
            }, null);
            _gap = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && _gap > frequency)
        {
            Bullet.Create(this, b =>
            {
                Rock.Create(b.transform.position, b.size);
            }, null);
            _gap = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && _gap > frequency)
        {
            Bullet.Create(this, b =>
            {
                Erosion.Create(b.transform.position, b.size);
            }, null);
            _gap = 0;
        }
    }

 
    
    protected override void Fire()
    {
        Bullet.Create(this, b =>
        {
            Explosion.Create(b.transform.position, b.size);
        }, null);
    }
}
