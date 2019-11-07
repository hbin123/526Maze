﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    [Range(0,1)]
    public float value;

    private Image bar;
    private const float MAXHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        this.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.value);
        bar.fillAmount = this.value;
    }


    public void setValue(int curHP)
    {
        this.value = curHP / MAXHP;
        //print(value);
    }

    public int getValue()
    {
        return (int)(this.value * MAXHP);
    }

}
