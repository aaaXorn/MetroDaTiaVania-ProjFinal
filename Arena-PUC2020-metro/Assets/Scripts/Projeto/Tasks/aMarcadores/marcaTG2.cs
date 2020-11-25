using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marcaTG2 : MonoBehaviour
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
		if(TS.tGas1Done)
			sRender.enabled = true;
		else
			sRender.enabled = false;
		
        if(TS.tGas2Done)
			Destroy(gameObject);
    }
}
