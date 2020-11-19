﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TasksScript : MonoBehaviour
{
	PhotonView pView;
	
	public GameObject Player;
	[SerializeField]
	public PlayerAmong PA;
	
	[SerializeField]
	GameObject TaskUsed, TaskHanoi, TaskGas1, TaskGas2, TaskChupeta1, TaskChupeta2;
	
	[SerializeField]
	bool taskCreated = false;
	
	public bool task = false;
	public string currentTask = "none";
	
	public bool taskGas2 = false, taskChupeta2 = false;//pra nao dar pra fazer a Gas2 antes da Gas1
	
	public bool tHanoiDone, tGas1Done, tGas2Done, tChu1Done, tChu2Done;//pras tasks feitas nao poderem ser feitas 2 vezes
    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
		//if(pView.IsMine)//com comentarios pra testar as tasks!!!!!
		//{
			if(task == true)
			{
				if(taskCreated == false)
				{
					switch(currentTask)
					{
						case "hanoi":
						if(tHanoiDone == false)
						{
							TaskUsed = Instantiate(TaskHanoi, transform.position, Quaternion.identity);
							TaskUsed.transform.parent = gameObject.transform;
							//pra virar um child gameObject e se mover com o objeto original
							tHanoiDone = true;
							taskCreated = true;
						}
						else
							task = false;
						break;
						
						case "gas1":
						if(tGas1Done == false)
						{
							TaskUsed = Instantiate(TaskGas1, transform.position, Quaternion.identity);
							TaskUsed.transform.parent = gameObject.transform;
							taskCreated = true;
						}
						else
							task = false;
						break;
						
						case "gas2":
						if(taskGas2 && tGas2Done == false)
						{
							TaskUsed = Instantiate(TaskGas2, transform.position, Quaternion.identity);
							TaskUsed.transform.parent = gameObject.transform;
							taskCreated = true;
						}
						else
							task = false;
						break;
						
						case "chupeta1":
						if(tChu1Done == false)
						{
							TaskUsed = Instantiate(TaskChupeta1, transform.position, Quaternion.identity);
							TaskUsed.transform.parent = gameObject.transform;
							taskCreated = true;
						}
						break;
						
						case "chupeta2":
						if(taskChupeta2 && tChu2Done == false)
						{
							TaskUsed = Instantiate(TaskChupeta2, transform.position, Quaternion.identity);
							TaskUsed.transform.parent = gameObject.transform;
							taskCreated = true;
						}
						break;
						
						default:
						task = false;
						break;
					}
				}
			}
			else
			{
				Destroy(TaskUsed);
				taskCreated = false;
			}
		//}
    }
	
	public void TaskMoney()
	{
		PA.money += 100;
	}
}
