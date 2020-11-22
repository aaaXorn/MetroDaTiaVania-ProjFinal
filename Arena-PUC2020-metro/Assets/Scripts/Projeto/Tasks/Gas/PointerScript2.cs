﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript2 : MonoBehaviour
{
    Animator anim;
	
	[SerializeField]
	TaskGas2 TG2;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Fuel", 3-TG2.timerCount);
    }
}