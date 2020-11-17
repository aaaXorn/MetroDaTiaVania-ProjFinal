using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
	Animator anim;
	
	[SerializeField]
	TaskGas1 TG1;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Fuel", TG1.timerCount);
    }
}
