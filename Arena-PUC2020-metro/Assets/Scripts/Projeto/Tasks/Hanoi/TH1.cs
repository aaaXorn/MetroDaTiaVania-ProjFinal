using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TH1 : MonoBehaviour
{
	Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		
		anim.SetBool("On", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
