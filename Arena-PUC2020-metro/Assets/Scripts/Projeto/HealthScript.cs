using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
	public GameObject Player;
	public PlayerAmong PA;
	
	SpriteRenderer vidaRender;
	
	[SerializeField]
	GameObject prefabHP;
	Stack<GameObject> healthIcons;
	
	float posX;
	
    // Start is called before the first frame update
    void Start()
    {
		vidaRender = prefabHP.GetComponent<SpriteRenderer>();
		
        healthIcons = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Heal()
	{
		posX = 3 * healthIcons.Count;
		healthIcons.Push(Instantiate(prefabHP, transform.position +
						 new Vector3(posX, 0, 0), Quaternion.identity,
						 gameObject.transform));
	}
	
	public void Dano(int damage)
	{
		PA.health -= damage;
		for(int i=0; i<damage; i++)
		{
			GameObject obj = healthIcons.Pop();
			Destroy(obj);
		}
	}
}
