﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raio1Script : MonoBehaviour
{
	Animator anim;
	
	[SerializeField]
	TaskChupeta1 TC1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TC1.timerStartV && TC1.timerStartP)
			anim.SetBool("On", true);
    }
}
