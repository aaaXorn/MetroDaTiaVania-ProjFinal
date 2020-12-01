using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInventario : MonoBehaviour
{
	[SerializeField]
	GameObject ItemTest;
	
	[SerializeField]
	InventarioScript IS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Teste()
	{
		IS.SetItem(ItemTest, 4);
	}
}
