using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioScript : MonoBehaviour
{
	public RectTransform content;
	public List<GameObject> itens;
	
	public bool teste1, teste2;
    // Start is called before the first frame update
    void Start()
    {
        
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
			(1.7f * (content.childCount + 1) + content.childCount, -1.7f, 0), Quaternion.identity) as GameObject;
			//"as GameObject" para fazer o Instantiate ser "como GameObject"
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
