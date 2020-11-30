using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marcaTCano : MonoBehaviour
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
        if(TS.tCanoDone)
			Destroy(gameObject);
    }
}
