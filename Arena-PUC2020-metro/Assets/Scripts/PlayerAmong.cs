using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmong : MonoBehaviour
{
	SpriteRenderer sRender;
	Rigidbody2D rb2D;
    PhotonView pView;
	
	[SerializeField]
	float inputX, inputY;//valor dos inputs, mudam de acordo com o input horizontal/vertical
	float speedX = 5, speedY = 5, speedD = 3.5f;//velocidade horizontal/vertical/diagonal base, speedD = 0.7x speed
	[SerializeField]
	float movementX, movementY;//velocidade usada
    // Start is called before the first frame update
    void Start()
    {
		sRender = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
		pView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pView.IsMine)
        {
			
		}
    }
	
	void FixedUpdate()
	{
		//if (pView.IsMine)
        //{
			Movement();
		//}
	}
	
	void Movement()
	{
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
			
		if (inputX > 0)//para o personagem nao parecer estar andando para um lado diferente do movimento
		{
			sRender.flipX = true;
		}
		else if (inputX < 0)
		{
			sRender.flipX = false;
		}
		
		if(inputX != 0 && inputY == 0  || inputX == 0 && inputY != 0)//movimento so pelo X ou so pelo Y
		{
			movementX = inputX * speedX;
			movementY = inputY * speedY;
		}
		else if (inputX != 0 && inputY != 0)//movimento na diagonal
		{
			movementX = inputX * speedD;
			movementY = inputY * speedD;
		}
		else if (inputX == 0 && inputY == 0)//parado
		{
			movementX = 0;
			movementY = 0;
		}
		
		rb2D.velocity = new Vector2(movementX, movementY);
	}
}
