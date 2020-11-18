using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TaskGas2 : MonoBehaviour
{
	PhotonView pView;
	
	[SerializeField]
	GameObject Tasks;
	[SerializeField]
	TasksScript TS;
	
	bool timerStart;
	
	float timer = 0;
	public int timerCount = 0;
	
	public bool Part2 = false;
	public bool moveGas = false;
	
	[SerializeField]
	bool taskEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
		
		//if(pView.IsMine)//com comentarios pra testar as tasks!!!!!
		//{
			Tasks = GameObject.FindWithTag("Tasks");
			TS = Tasks.GetComponent<TasksScript>();
		//}
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStart == true)
		{
			timer += Time.deltaTime;
			
			if(timer>1 && timerCount<4)
			{
				timerCount++;
				timer = 0;
			}
			
			if(timerCount>3)
			{
				taskEnd = true;
				timerStart = false;
				TS.TaskMoney();
				TS.task = false;
			}
		}
    }
	
	public void Gas2Part1()
	{
		Part2 = true;
	}
		
	public void Gas2Part2()
	{
		if(Part2 == true)
		{
			timerStart = true;
			moveGas = true;
		}
	}
}
