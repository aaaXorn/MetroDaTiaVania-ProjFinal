using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TaskChupeta1 : MonoBehaviour
{
	PhotonView pView;
	
	[SerializeField]
	GameObject Tasks;
	[SerializeField]
	TasksScript TS;
	
	int cor = 0;//0 e nada, 1 e vermelho, 2 e preto
	
	public bool timerStartV, timerStartP;
	float timer = 0;
	int timerCount = 0;
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
        if(timerStartV && timerStartP)
		{
			timer += Time.deltaTime;
			
			if(timer>1 && timerCount<5)
			{
				timerCount++;
				timer = 0;
			}
			
			if(timerCount>4)
			{
				timerStartV = false;
				timerStartP = false;
				TS.taskGas2 = true;
				TS.tChu1Done = true;
				TS.TaskMoney();
				TS.task = false;
			}
		}
    }
	
	public void Vermelho()
	{
		cor = 1;
	}
	
	public void Preto()
	{
		cor = 2;
	}
	
	public void FioV()
	{
		if(cor == 1)
		{
			timerStartV = true;
		}
	}
	
	public void FioP()
	{
		if(cor == 2)
		{
			timerStartP = true;
		}
	}
}
