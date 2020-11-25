using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marcaTC2 : MonoBehaviour
{
    SpriteRenderer sRender;
	
	[SerializeField]
	TasksScript TS;
    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if(TS.tChu1Done)
			sRender.enabled = true;
		else
			sRender.enabled = false;
		
        if(TS.tChu2Done)
			Destroy(gameObject);
    }
}
