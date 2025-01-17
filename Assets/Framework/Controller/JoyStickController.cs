using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public RectTransform joyStick;
    public RectTransform joyStickPannel;
    // 默认图片是正方形
    private float Size => joyStickPannel.rect.width * 0.5f;
    private Action<Vector2> _onMove;
    private Vector2 _downPos = Vector2.zero;
    private bool _isDrag = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _downPos = Input.mousePosition;
        _isDrag = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        joyStick.localPosition = Vector2.zero;
        _isDrag = false;
    }

    public void Activate(Action<Vector2> onMove)
    {
        this._onMove = onMove;
        enabled = true;
    }

    public void Inactivate()
    {
        this._onMove = null;
        enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isDrag) 
            return;
        
        var vec = (Vector2)Input.mousePosition - _downPos;
        vec = vec.magnitude < Size ? vec : vec.normalized * Size;
        _onMove?.Invoke(vec / Size);
        joyStick.localPosition = vec;
    }
}
