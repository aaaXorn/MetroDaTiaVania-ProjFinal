using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCano : MonoBehaviour
{
	[SerializeField]
	GameObject Tasks;
	[SerializeField]
	TasksScript TS;
	
	[SerializeField]
	int cano, acertos;
	
	public bool c1, c2, c3, c4, c5;
    // Start is called before the first frame update
    void Start()
    {
        Tasks = GameObject.FindWithTag("Tasks");
		TS = Tasks.GetComponent<TasksScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(acertos>=5)
		{
			TS.tCanoDone = true;
			TS.TaskMoney();
			TS.task = false;
		}
    }
	
	public void Cano1()
	{
		if(cano == 2)
		{
			if(c1 == false)
			{
				acertos++;
				c1 = true;
			}
		}
	}
	public void Cano2()
	{
		if(cano == 4)
		{
			if(c2 == false)
			{
				acertos++;
				c2 = true;
			}
		}
	}
	public void Cano3()
	{
		if(cano == 3)
		{
			if(c3 == false)
			{
				acertos++;
				c3 = true;
			}
		}
	}
	public void Cano4()
	{
		if(cano == 2)
		{
			if(c4 == false)
			{
				acertos++;
				c4 = true;
			}
		}
	}
	public void Cano5()
	{
		if(cano == 3)
		{
			if(c5 == false)
			{
				acertos++;
				c5 = true;
			}
		}
	}
	
	public void SetCano1()
	{
		cano = 1;
	}
	public void SetCano2()
	{
		cano = 2;
	}
	public void SetCano3()
	{
		cano = 3;
	}
	public void SetCano4()
	{
		cano = 4;
	}
}
