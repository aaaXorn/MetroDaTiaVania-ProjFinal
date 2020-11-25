using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStartWall : MonoBehaviour
{
	[SerializeField]
	RoleScript RS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(RS.start)
			Destroy(gameObject);
    }
}
