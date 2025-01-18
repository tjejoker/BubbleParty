using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : GunBase
{
    public float speedMin;
    public float speedMax;

    public float sizeMin;
    public float sizeMax;

    public float lifeTimeMin;
    public float lifeTimeMax;

    public float amplitudeMin;
    public float amplitudeMax;

    public float offsetAngleMin;
    public float offsetAngleMax;
    
    public AnimationCurve speedFactor;
    public AnimationCurve sizeFactor;
    public AnimationCurve amplitudeFactor;

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        _gap += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && _gap > frequency)
        {
            Fire();
            _gap = 0;
        }
    }


    protected override void Fire()
    {
        Bubble.CreateBubble(this);
    }
}