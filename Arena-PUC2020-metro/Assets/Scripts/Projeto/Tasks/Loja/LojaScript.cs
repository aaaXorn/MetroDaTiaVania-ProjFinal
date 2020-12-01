using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LojaScript : MonoBehaviour
{
	[SerializeField]
	GameObject Tasks, Vidas, Inventario, Espada;
	[SerializeField]
	TasksScript TS;
	[SerializeField]
	HealthScript HS;
	
	int repairHeal;
	
	[SerializeField]
	InventarioScript IS;
    // Start is called before the first frame update
    void Start()
    {
        Tasks = GameObject.FindWithTag("Tasks");
		TS = Tasks.GetComponent<TasksScript>();
		
		Vidas = GameObject.FindWithTag("Vidas");
		HS = Vidas.GetComponent<HealthScript>();
		
		Inventario = GameObject.FindWithTag("Inventario");
		IS = Inventario.GetComponent<InventarioScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Sword()
	{
		if(TS.PA.money>=100)
		{
			TS.PA.money -= 100;
			IS.SetItem(Espada, 2);
		}
	}
	
	public void Repair()
	{
		if(TS.PA.health>0 && TS.PA.money>=250)
		{
			TS.PA.money -= 250;
			
			repairHeal = TS.PA.maxHealth - TS.PA.health;
			for(int i=0; i<repairHeal; i++)
			{
				HS.Heal();
			}
			
			TS.PA.health = TS.PA.maxHealth;
		}
	}
	
	public void Exit()
	{
		TS.taskCreated = false;
		TS.task = false;
		TS.PA.mayMove = true;
		TS.PA.mayAttack = true;
	}
}
