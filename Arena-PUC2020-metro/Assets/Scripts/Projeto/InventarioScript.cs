using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioScript : MonoBehaviour
{
	public GameObject Player;
	public PlayerAmong PA;
	public PlayerAttacks PAtk;
	
	public RectTransform content;
	public List<GameObject> itens;
	
	[SerializeField]
	GameObject fundoInventario, ArmaBase;
	
    // Start is called before the first frame update
    void Start()
    {
        GameObject Barra = Instantiate(fundoInventario, content.position + new Vector3(12.8f, -1.1f, 0), Quaternion.identity);
		Barra.transform.parent = gameObject.transform;
		
		SetItem(ArmaBase, 1);
    }

    // Update is called once per frame
    void Update()
    {
        AddItem();
    }
	
	void AddItem()
	{
		if(content.childCount<itens.Count)
		{
			GameObject Item = Instantiate(itens[content.childCount], content.position + new Vector3
			(3 * (content.childCount + 1) + content.childCount, -1.7f, 0), Quaternion.identity) as GameObject;
			//"as GameObject" para fazer o Instantiate ser "como GameObject"
			Item.transform.parent = content.transform;
		}
	}
	
	public void SetItem(GameObject itemNovo, int arma)
	{
		if(itens.Count<5)
		{
			itens.Add(itemNovo);
			
			switch(content.childCount)
			{
				case 0:
				PAtk.arma1 = arma;
				break;
				
				case 1:
				PAtk.arma2 = arma;
				break;
				
				case 2:
				PAtk.arma3 = arma;
				break;
				
				case 3:
				PAtk.arma4 = arma;
				break;
				
				case 4:
				PAtk.arma5 = arma;
				break;
				
				default:
				break;
			}
		}
	}
}
