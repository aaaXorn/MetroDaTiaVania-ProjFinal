using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PretoScript : MonoBehaviour
{
    [SerializeField]
	TaskChupeta1 TC1;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TC1.timerStartP)
			Destroy(gameObject);
    }
}
