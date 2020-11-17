using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manguera : MonoBehaviour
{
	Animator anim;
	
	public bool Part2 = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void MangueraPart1()
	{
		Part2 = true;
	}
	
	public void AnimChange()
	{
		anim.SetBool("Start", true);
	}
}
