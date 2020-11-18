﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FioP : MonoBehaviour
{
    [SerializeField]
	TaskChupeta1 TC1;
	
	SpriteRenderer sRender;
	
    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TC1.timerStartP)
			sRender.enabled = true;
    }
}
