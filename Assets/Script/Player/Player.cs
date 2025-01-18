using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCtrl
{
    Player1, Player2
}

public class Player : MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    
    private Rigidbody2D rb;

    public float speed;
    private float inputX;
    private float inputY;

    private Vector2 movementInput;

    // 移动范围限制参数
    private float minX = -60f; // X轴最小值
    private float maxX = 60f;  // X轴最大值
    private float minY = -30f; // Y轴最小值
    private float maxY = 19f;  // Y轴最大值

    // 枪的控制参数
    private Vector2 gunDirection = Vector2.right; // 枪的默认方向（向右）
    private float gunRadius = 4.6f; // 枪与Body的距离

    private Transform _gun; // 枪的Transform组件
    private float _angle;
    
    public BubbleGun bubbleGun;
    public List<GunBase> guns;
    private Queue<GunBase> _guns;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 锁定旋转
        
        _gun = bubbleGun.transform;
        guns.ForEach(g => g.gameObject.SetActive(false));
        _guns = new Queue<GunBase>(guns);
    }

    private void PlayerInput()
    {
        switch (playerCtrl)
        {
            case PlayerCtrl.Player1:
                inputX = Input.GetAxisRaw("Horizontal_WASD");
                inputY = Input.GetAxisRaw("Vertical_WASD");
                
                if(Input.GetKey(KeyCode.J))
                    FireBubble();
                if(Input.GetKey(KeyCode.K))
                    FireSpecial();
                if(Input.GetKey(KeyCode.L))
                    SwitchGun();
                
                break;
            case PlayerCtrl.Player2:
                inputX = Input.GetAxisRaw("Horizontal_Arrows");
                inputY = Input.GetAxisRaw("Vertical_Arrows");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        

        // 如果同时按下两个方向键，减少移动速度以避免对角线移动过快
        if (inputX != 0 && inputY != 0)
        {
            inputX = inputX * 0.6f;
            inputY = inputY * 0.6f;
        }
        movementInput = new Vector2(inputX, inputY);
    }

    private void Movement()
    {
        // 计算新位置
        Vector2 newPosition = rb.position + movementInput * (speed * Time.deltaTime);

        // 限制新位置在矩形区域内
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // 应用新位置
        rb.MovePosition(newPosition);
    }

    private void Update()
    {
        PlayerInput();
        UpdateGunPositionAndRotation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void UpdateGunPositionAndRotation()
    {
        if (_gun == null)
        {
            Debug.LogError("Gun is not assigned in the inspector!");
            return;
        }

        if (movementInput != Vector2.zero)
        {
            // 根据输入方向更新枪的方向
            gunDirection = movementInput.normalized;
            _angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

            SetGunRot();
        }
    }

    private void FireBubble()
    {
        _guns.Peek().gameObject.SetActive(false);
        bubbleGun.gameObject.SetActive(true);

        _gun = bubbleGun.transform;
        SetGunRot();
        
        bubbleGun.Fire();
    }

    private void FireSpecial()
    {
        bubbleGun.gameObject.SetActive(false);
        _guns.Peek().gameObject.SetActive(true);
        
        _gun = _guns.Peek().transform;
        SetGunRot();

        _guns.Peek().Fire();
        
    }
    public void SwitchGun()
    {
        bubbleGun.gameObject.SetActive(false);
        _guns.Peek().gameObject.SetActive(false);
        _guns.Enqueue(_guns.Dequeue());
        _guns.Peek().gameObject.SetActive(true);
        
        _gun = _guns.Peek().transform;
        SetGunRot();
    }

    private void SetGunRot()
    {
        // 计算枪的位置
        Vector2 gunOffset = gunDirection * gunRadius;
        _gun.position = rb.position + gunOffset;

        // 计算枪的旋转角度
        _gun.rotation = Quaternion.Euler(0, 0, _angle);

        // 水平翻转枪的图片
        // 水平翻转 or 恢复默认
        
        _gun.localScale = gunDirection.x < 0 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1); 
    }
    
}
