using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockStateRemove : MonoBehaviour
{
	[SerializeField]
	GamePlay GP;
    // Start is called before the first frame update
    void Start()
    {
        GP.cursorOff = true;
		Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
