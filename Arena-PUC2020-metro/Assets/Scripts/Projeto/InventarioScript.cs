using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioScript : MonoBehaviour
{
	public RectTransform content;
	public List<GameObject> itens;
	
	[SerializeField]
	GameObject fundoInventario;
	
	public bool teste1, teste2;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Barra = Instantiate(fundoInventario, content.position + new Vector3(12.8f, -1.1f, 0), Quaternion.identity);
		Barra.transform.parent = gameObject.transform;
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
			//versao antiga 1.7f*(content.childCount+1)
			Item.transform.parent = content.transform;
		}
	}
	
	public void SetItem(GameObject itemNovo)
	{
		if(itens.Count<5)
		{
			teste2 = true;
			itens.Add(itemNovo);
		}
	}
}
