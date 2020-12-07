using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LojaScript : MonoBehaviour
{
	[SerializeField]
	GameObject Tasks, Vidas, Inventario, Espada, Taser, Ima, Shield, Martelo, Cura;
	[SerializeField]
	TasksScript TS;
	[SerializeField]
	HealthScript HS;
	
	int repairHeal;
	
	bool bateriaBuy;
	
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
	
	public void Hammer()
	{
		if(TS.PA.money>=100)
		{
			TS.PA.money -= 100;
			IS.SetItem(Martelo, 5);
		}
	}
	
	public void Choquinho()
	{
		if(TS.PA.money>=100)
		{
			TS.PA.money -= 100;
			IS.SetItem(Taser, 3);
		}
	}
	
	public void ImaSlow()
	{
		if(TS.PA.money>=150)
		{
			TS.PA.money -= 150;
			IS.SetItem(Ima, 4);
		}
	}
	
	public void Escudo()
	{
		if(TS.PA.money>=200)
		{
			TS.PA.money -= 200;
			IS.SetItem(Shield, -1);
		}
	}
	
	public void Bateria()
	{
		if(TS.PA.money>=200 && bateriaBuy == false)
		{
			TS.PA.money -= 200;
			TS.PA.multiplierTotal = 1.2f;
		}
	}
	
	public void Medkit()
	{
		if(TS.PA.money>=75)
		{
			TS.PA.money -= 75;
			IS.SetItem(Cura, -2);
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
