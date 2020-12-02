using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFios : MonoBehaviour
{
	[SerializeField]
	GameObject Tasks;
	[SerializeField]
	TasksScript TS;
	
	[SerializeField]
	int fio, acertos;
	
	[SerializeField]
	SpriteRenderer fi1, fi2, fi3, fi22, fi4, ca1, ca2, ca3, ca4, ca5;
	
	[SerializeField]
	bool f1, f2, f3, f4, f5, fio2, fio22;
	
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
			TS.tFiosDone = true;
			TS.TaskMoney();
			TS.task = false;
		}
    }
	
	public void Fio1()
	{
		if(fio == 2 && fio2 == false)
		{
			if(f1 == false)
			{
				acertos++;
				ca1.enabled = true;
				f1 = true;
				fi2.enabled = false;
				fio2 = true;
			}
		}
		else if(fio == 22 && fio22 == false)
		{
			if(f1 == false)
			{
				acertos++;
				ca1.enabled = true;
				f1 = true;
				fi22.enabled = false;
				fio22 = true;
			}
		}
	}
	
	public void Fio2()
	{
		if(fio == 4)
		{
			if(f2 == false)
			{
				acertos++;
				ca2.enabled = true;
				fi4.enabled = false;
				f2 = true;
			}
		}
	}
	
	public void Fio3()
	{
		if(fio == 3)
		{
			if(f3 == false)
			{
				acertos++;
				ca3.enabled = true;
				fi3.enabled = false;
				f3 = true;
			}
		}
	}
	
	public void Fio4()
	{
		if(fio == 1)
		{
			if(f4 == false)
			{
				acertos++;
				ca4.enabled = true;
				fi1.enabled = false;
				f4 = true;
			}
		}
	}
	
	public void Fio5()
	{
		if(fio == 2 && fio2 == false)
		{
			if(f5 == false)
			{
				acertos++;
				ca5.enabled = true;
				fi2.enabled = false;
				f5 = true;
				fio2 = true;
			}
		}
		else if(fio == 22 && fio22 == false)
		{
			if(f5 == false)
			{
				acertos++;
				ca5.enabled = true;
				fi22.enabled = false;
				f5 = true;
				fio22 = true;
			}
		}
	}
	
	public void SetFio1()
	{
		fio = 1;
	}
	public void SetFio2()
	{
		fio = 2;
	}
	public void SetFio3()
	{
		fio = 3;
	}
	public void SetFio22()
	{
		fio = 22;
	}
	public void SetFio4()
	{
		fio = 4;
	}
}
