using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGasGas : MonoBehaviour
{
	Animator anim;
	
	bool moved = false;
	
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
        if(TG2.Part2)
			anim.SetBool("Clicked", true);
		if(TG2.moveGas && moved == false)
		{
			transform.Translate(11.1f, 6.6f, 0);
			moved = true;
		}
    }
}
