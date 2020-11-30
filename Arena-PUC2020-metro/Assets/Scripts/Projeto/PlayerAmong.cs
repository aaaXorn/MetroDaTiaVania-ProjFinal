﻿using Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmong : MonoBehaviourPunCallbacks, IPunObservable
{//, IPunObservable para passar variaveis com o PhotonView
	Animator anim;
	SpriteRenderer sRender;
	Rigidbody2D rb2D;
    PhotonView pView;
	BoxCollider2D bc2D;
	
	[SerializeField]
	PlayerAttacks PAtk;
	[SerializeField]
	PlayerCabeca PC;
	[SerializeField]
	GameObject MainCamera, VirtualCamera, Tasks, RoleSet, HealthRTr, Inventario;
	[SerializeField]
	PlayerCameraScript PCS;
	[SerializeField]
	VCameraScript VCS;
	[SerializeField]
	TasksScript TS;
	[SerializeField]
	RoleScript RS;
	[SerializeField]
	HealthScript HS;
	[SerializeField]
	InventarioScript IS;
	
	public int health = 5;
	public int maxHealth = 5;
	public bool alive = true;
	public bool mayMove = true;
	public bool mayAttack = true;
	
	public bool mayBeAttacked = false;
	
	public string role;
	bool roleCall = false;
	float roleCallTimer = 4;
	[SerializeField]
	GameObject Role, RoleInocente, RoleDetetive, RoleSabotador;
	public bool roleOK = false;
	public bool skinOK = false;
	
	public int money = 0;
	
	[SerializeField]
	float inputX, inputY;//valor dos inputs, mudam de acordo com o input horizontal/vertical
	float speedX = 7, speedY = 7, speedD = 4.9f;//velocidade horizontal/vertical/diagonal base, speedD = 0.7x speed
	[SerializeField]
	float movementX, movementY;//velocidade usada
	
	public bool shoot = false;
	public Vector3 cursorDistance;//distancia entre o player e a posicao do cursor
	public float cursorMagnitude;//tamanho do vetor cursorDistance, usado em calculos
	public float directionZ;//direcao da reta entre o player e a posicao do cursor
	public Vector2 attackDirection;//direcao que o ataque ganha velocidade
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
		sRender = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
		pView = GetComponent<PhotonView>();
		bc2D = GetComponent<BoxCollider2D>();
		
		if(pView.IsMine)
		{
			RoleSet = GameObject.FindWithTag("RoleSet");
			RS = RoleSet.GetComponent<RoleScript>();
			
			RS.Player = gameObject;
			RS.PA = RS.Player.GetComponent<PlayerAmong>();
			
			RS.maisPlayer();
			
			HealthRTr = GameObject.FindWithTag("Vidas");
			HS = HealthRTr.GetComponent<HealthScript>();
			
			HS.Player = gameObject;
			HS.PA = HS.Player.GetComponent<PlayerAmong>();
			
			Inventario = GameObject.FindWithTag("Inventario");
			IS = Inventario.GetComponent<InventarioScript>();
			
			IS.Player = gameObject;
			IS.PA = IS.Player.GetComponent<PlayerAmong>();
			IS.PAtk = PAtk;
			
			MainCamera = GameObject.FindWithTag("MainCamera");//necessario ja que o jogador e criado com Instantiate
			VirtualCamera = GameObject.FindWithTag("VirtualCamera");
			Tasks = GameObject.FindWithTag("Tasks");
			PCS = MainCamera.GetComponent<PlayerCameraScript>();
			VCS = VirtualCamera.GetComponent<VCameraScript>();
			TS = Tasks.GetComponent<TasksScript>();
			
			TS.Player = gameObject;
			TS.PA = TS.Player.GetComponent<PlayerAmong>();
			
			for(int i=0; i<health; i++)
			{
				HS.Heal();
			}
		}
		
		//Cursor.visible = false;//tira o cursor pra deixar so a crosshair
    }

    // Update is called once per frame
    void Update()
    {
        if (pView.IsMine)
        {
			if(alive)
			{
				VCS.CameraFollow(gameObject);
			
				MouseVariables();
			
				if(health<=0)
				{
					if(role == "inocente")
						RS.inocenMorre();
					else if(role == "sabotador")
						RS.sabotaMorre();
					pView.RPC("RPC_Death", RpcTarget.All);
				}
				else if(health>maxHealth)
				{
					health = maxHealth;
				}
			}
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
			
			if(roleCallTimer>0)
			{
				if(roleCall == true)
					roleCallTimer -= Time.deltaTime;
			}
			else if(roleCallTimer<=0 && roleCall == true)
			{
				roleCall = false;
			}
		}
    }
	
	void FixedUpdate()
	{
		if (pView.IsMine)
        {
			if(alive)
			{
				if(mayMove)
				{
					Movement();
				}
				else
				{
					rb2D.velocity = new Vector2(0, 0);
				}
			}
		}
	}
	
	#region IPunObservable implementation
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.IsWriting && pView.IsMine)//manda as variaveis pros outros jogadores
		{
			stream.SendNext(health);
			stream.SendNext(shoot);
			stream.SendNext(directionZ);
			stream.SendNext(cursorDistance);
			stream.SendNext(attackDirection);
			stream.SendNext(role);
		}
		else//recebe as variaveis
		{
			health = (int)stream.ReceiveNext();
			shoot = (bool)stream.ReceiveNext();
			directionZ = (float)stream.ReceiveNext();
			cursorDistance = (Vector3)stream.ReceiveNext();
			attackDirection = (Vector2)stream.ReceiveNext();
			role = (string)stream.ReceiveNext();
		}
	}
	
	#endregion
	
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
	
	void MouseVariables()
	{
		cursorDistance = PCS.mousePos - transform.position;
			
		directionZ = Mathf.Atan2(cursorDistance.y, cursorDistance.x) * Mathf.Rad2Deg;
		//Atan2 pega o angulo, Rag2Deg transforma em graus
	}
	
	public void RoleCall()
	{
		roleOK = true;
		roleCall = true;
		switch(role)
		{
			case "inocente":
			Role = Instantiate(RoleInocente, transform.position, Quaternion.identity);
			Role.transform.parent = gameObject.transform;
			break;
			
			case "detetive":
			Role = Instantiate(RoleDetetive, transform.position, Quaternion.identity);
			Role.transform.parent = gameObject.transform;
			break;
			
			case "sabotador":
			Role = Instantiate(RoleSabotador, transform.position, Quaternion.identity);
			Role.transform.parent = gameObject.transform;
			money += 300;
			break;
		}
	}
	
	//funcoes de RPC pro online funcionar
	
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
	
	[PunRPC]
	void RPC_Death()
	{
		anim.SetBool("Morto", true);
		alive = false;
		bc2D.enabled = false;
		PC.OnDeath();
	}
	
	//funcoes de colisao e trigger
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(pView.IsMine)
		{
			if(mayBeAttacked)
				Hit(collision);
			
			if(role == "inocente")
			{
				AtivaTasks(collision);
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D collision)
	{
		if(pView.IsMine)
		{
			if(collision.gameObject.tag == "RoleSet")
			{
				if(roleOK == false)
					RS.SetRoles();
				if(skinOK == false)
					RS.SetSkin();
			}
		}
	}
	
	void AtivaTasks(Collider2D collision)
	{
		if (collision.gameObject.tag == "TG1")
		{
			TS.task = true;
			TS.currentTask = "gas1";
		}
		else if (collision.gameObject.tag == "TG2")
		{
			TS.task = true;
			TS.currentTask = "gas2";
		}
		else if(collision.gameObject.tag == "TC1")
		{
			TS.task = true;
			TS.currentTask = "chupeta1";
		}
		else if(collision.gameObject.tag == "TC2")
		{
			TS.task = true;
			TS.currentTask = "chupeta2";
		}
		else if(collision.gameObject.tag == "TEo")
		{
			TS.task = true;
			TS.currentTask = "eolica";
		}
	}
	
	void Hit(Collider2D collision)
	{
		if(collision.gameObject.tag == "Bullet0")
		{
			HS.Dano(1);
		}
		
		if(collision.gameObject.tag == "Dano2")
		{
			HS.Dano(2);
		}
	}
}
