using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectate : MonoBehaviour
{
	Rigidbody2D rb2D;
	
	float inputX, inputY;
	float speedX = 10, speedY = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
		
		rb2D.velocity = new Vector2(speedX * inputX, speedY * inputY);
	}
}
