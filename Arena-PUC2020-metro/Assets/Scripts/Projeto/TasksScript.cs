using System.Collections;
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
	GameObject TaskUsed, TaskHanoi, TaskGas1;
	
	[SerializeField]
	bool taskCreated = false;
	
	public bool task = false;
	public string currentTask = "none";
	
	public int taskPoints = 0;
	
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
						TaskUsed = Instantiate(TaskHanoi, transform.position, Quaternion.identity);
						TaskUsed.transform.parent = gameObject.transform;//pra virar um child gameObject e se mover com o objeto original
						taskCreated = true;
						break;
						
						case "gas1":
						TaskUsed = Instantiate(TaskGas1, transform.position, Quaternion.identity);
						TaskUsed.transform.parent = gameObject.transform;
						taskCreated = true;
						break;
						
						default:
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
	
	public void PlusTaskPoint()
	{
		pView.RPC("RPC_TaskPoint", RpcTarget.All);
	}
	
	[PunRPC]
	void RPC_TaskPoint()
	{
		taskPoints++;
	}
}
