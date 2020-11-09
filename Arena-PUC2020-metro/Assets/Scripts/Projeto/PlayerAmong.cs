using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmong : MonoBehaviour
{
	Animator anim;
	SpriteRenderer sRender;
	Rigidbody2D rb2D;
    PhotonView pView;
	
	[SerializeField]
	GameObject MainCamera, VirtualCamera;
	[SerializeField]
	PlayerCameraScript PCS;
	[SerializeField]
	VCameraScript VCS;
	
	int health;
	int maxHealth;
	bool alive = true;
	public bool task = false;
	bool mayMove = true;
	public bool mayAttack = true;
	
	[SerializeField]
	float inputX, inputY;//valor dos inputs, mudam de acordo com o input horizontal/vertical
	float speedX = 5, speedY = 5, speedD = 3.5f;//velocidade horizontal/vertical/diagonal base, speedD = 0.7x speed
	[SerializeField]
	float movementX, movementY;//velocidade usada
	
	public Vector3 cursorDistance;
	public float cursorMagnitude;
	public float directionZ;
	public Vector2 attackDirection;//direçao usada no script PlayerAttacks
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
		sRender = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
		pView = GetComponent<PhotonView>();
		
		MainCamera = GameObject.FindWithTag("MainCamera");
		VirtualCamera = GameObject.FindWithTag("VirtualCamera");
		PCS = MainCamera.GetComponent<PlayerCameraScript>();
		VCS = VirtualCamera.GetComponent<VCameraScript>();
		
		//Cursor.visible = false;//tira o cursor pra deixar so a crosshair
    }

    // Update is called once per frame
    void Update()
    {
        if (pView.IsMine)
        {
			VCS.CameraFollow(gameObject);
				
			pView.RPC("RPC_MouseVariables", RpcTarget.All);
		}
		
		if(alive)
		{
			if(health<=0)
			{
				Death();
			}
			else if(health>maxHealth)
			{
				health = maxHealth;
			}
		}
		else
		{
			
		}
    }
	
	void FixedUpdate()
	{
		if (pView.IsMine)
        {
			if(mayMove)
			{
				Movement();
			}
		}
	}
	
	void Movement()
	{
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
			
		if (inputX > 0)//para o personagem nao parecer estar andando para um lado diferente do movimento
		{
			pView.RPC("RPC_renderFlipTrue", RpcTarget.All);
		}
		else if (inputX < 0)
		{
			pView.RPC("RPC_renderFlipFalse", RpcTarget.All);
		}
		
		if(inputX != 0 && inputY == 0  || inputX == 0 && inputY != 0)//movimento so pelo X ou so pelo Y
		{
			pView.RPC("RPC_animMoverTrue", RpcTarget.All);
			movementX = inputX * speedX;
			movementY = inputY * speedY;
		}
		else if (inputX != 0 && inputY != 0)//movimento na diagonal
		{
			pView.RPC("RPC_animMoverTrue", RpcTarget.All);
			movementX = inputX * speedD;
			movementY = inputY * speedD;
		}
		else if (inputX == 0 && inputY == 0)//parado
		{
			pView.RPC("RPC_animMoverFalse", RpcTarget.All);
			movementX = 0;
			movementY = 0;
		}
		
		rb2D.velocity = new Vector2(movementX, movementY);
	}
	
	void Death()
	{
		
	}
	
	//funcoes de RPC pro online funcionar
	[PunRPC]
	void RPC_MouseVariables()
	{
		cursorDistance = PCS.mousePos - transform.position;
			
		directionZ = Mathf.Atan2(cursorDistance.y, cursorDistance.x) * Mathf.Rad2Deg;
		//Atan2 pega o angulo, Rag2Deg transforma em graus
	}
	
	//animacao
		[PunRPC]
		void RPC_renderFlipTrue()
		{
			sRender.flipX = true;
		}
		[PunRPC]
		void RPC_renderFlipFalse()
		{
			sRender.flipX = false;
		}
		[PunRPC]
		void RPC_animMoverTrue()
		{
			anim.SetBool("Mover", true);
		}
		[PunRPC]
		void RPC_animMoverFalse()
		{
			anim.SetBool("Mover", false);
		}
}
