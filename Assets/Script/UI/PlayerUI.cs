using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image img_HP;
    public Text txt_HP;

    private void Start()
    {
        img_HP = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        txt_HP = transform.GetChild(2).GetComponent<Text>();
    }

    public void SetText(float hpPercent)
    {
        txt_HP.text = Math.Round( hpPercent,2).ToString() + " %";
    }

    public void SetHP(float hp)
    {
        img_HP.fillAmount = 1 - (hp / 100f);
    }
    
}
