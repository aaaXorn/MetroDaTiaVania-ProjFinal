﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sRC4 : MonoBehaviour
{
    [SerializeField]
	TaskCano TC;
	
	SpriteRenderer sRender;
    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TC.c4)
			sRender.enabled = true;
    }
}