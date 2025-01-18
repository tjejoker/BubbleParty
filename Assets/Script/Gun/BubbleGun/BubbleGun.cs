using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleGun : GunBase
{
    public float frequency = 0.02f;

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

    private float _gap = 0;

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

    public override void Fire()
    {
        Bubble.CreateBubble(this);
    }
}