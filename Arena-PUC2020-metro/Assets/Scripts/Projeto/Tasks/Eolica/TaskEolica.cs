using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEolica : MonoBehaviour
{
	[SerializeField]
	GameObject Tasks;
	[SerializeField]
	TasksScript TS;
	
	public int ferrugens = 3;
	public bool spin;
	
	float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        Tasks = GameObject.FindWithTag("Tasks");
		TS = Tasks.GetComponent<TasksScript>();
    }

    // Update is called once per frame
    void Update()
    {
		if(ferrugens<=0)
			spin = true;
		
        if(spin)
		{
			timer-=Time.deltaTime;
			if(timer<=0)
			{
				TS.tEolicaDone = true;
				TS.TaskMoney();
				TS.task = false;
			}
		}
    }
}
