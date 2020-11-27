using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
	Camera camera;
	
	[SerializeField]
	GameObject Mapa;
	[SerializeField]
	SpriteRenderer mapaRender;
	
	public Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
		
		mapaRender = Mapa.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
															Input.mousePosition.y, transform.position.z));
															
		if(Input.GetKeyDown(KeyCode.M))
		{
			if(mapaRender.enabled == true)
				mapaRender.enabled = false;
			else
				mapaRender.enabled = true;
		}
    }
}
