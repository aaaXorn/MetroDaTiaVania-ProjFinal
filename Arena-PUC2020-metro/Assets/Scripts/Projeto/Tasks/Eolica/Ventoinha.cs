using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventoinha : MonoBehaviour
{
	Animator anim;
	
	[SerializeField]
	GameObject Ferrugem1, Ferrugem2, Ferrugem3;
	
	[SerializeField]
	TaskEolica TE;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Spin", TE.spin);
    }
	
	public void Ferr1()
	{
		TE.ferrugens--;
		Destroy(Ferrugem1);
	}
	
	public void Ferr2()
	{
		TE.ferrugens--;
		Destroy(Ferrugem2);
	}
	
	public void Ferr3()
	{
		TE.ferrugens--;
		Destroy(Ferrugem3);
	}
}
