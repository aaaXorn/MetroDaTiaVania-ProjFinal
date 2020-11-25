using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marcaTG1 : MonoBehaviour
{
	[SerializeField]
	TasksScript TS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TS.tGas1Done)
			Destroy(gameObject);
    }
}
