using Photon;
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
	
	[SerializeField]
	GameObject MainCamera, VirtualCamera, Tasks;
	[SerializeField]
	PlayerCameraScript PCS;
	[SerializeField]
	VCameraScript VCS;
	[SerializeField]
	TasksScript TS;
	
	int health;
	int maxHealth;
	bool alive = true;
	bool mayMove = true;
	public bool mayAttack = true;
	
	public int money = 0;
	
	[SerializeField]
	float inputX, inputY;//valor dos inputs, mudam de acordo com o input horizontal/vertical
	float speedX = 5, speedY = 5, speedD = 3.5f;//velocidade horizontal/vertical/diagonal base, speedD = 0.7x speed
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
		
		//if(pView.IsMine)//com comentarios pra testar as tasks!!!!!
		//{
			MainCamera = GameObject.FindWithTag("MainCamera");//necessario ja que o jogador e criado com Instantiate
			VirtualCamera = GameObject.FindWithTag("VirtualCamera");
			Tasks = GameObject.FindWithTag("Tasks");
			PCS = MainCamera.GetComponent<PlayerCameraScript>();
			VCS = VirtualCamera.GetComponent<VCameraScript>();
			TS = Tasks.GetComponent<TasksScript>();
			
			TS.Player = gameObject;
			TS.PA = TS.Player.GetComponent<PlayerAmong>();
		//}
		
		//Cursor.visible = false;//tira o cursor pra deixar so a crosshair
    }

    // Update is called once per frame
    void Update()
    {
        if (pView.IsMine)
        {
			VCS.CameraFollow(gameObject);
			
			MouseVariables();
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
		}
		else//recebe as variaveis
		{
			health = (int)stream.ReceiveNext();
			shoot = (bool)stream.ReceiveNext();
			directionZ = (float)stream.ReceiveNext();
			cursorDistance = (Vector3)stream.ReceiveNext();
			attackDirection = (Vector2)stream.ReceiveNext();
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
	
	void Death()
	{
		
	}
	
	void MouseVariables()
	{
		cursorDistance = PCS.mousePos - transform.position;
			
		directionZ = Mathf.Atan2(cursorDistance.y, cursorDistance.x) * Mathf.Rad2Deg;
		//Atan2 pega o angulo, Rag2Deg transforma em graus
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
		
	//funcoes de colisao e trigger
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "TG1")
		{
			TS.task = true;
			TS.currentTask = "gas1";
		}
		if (collision.gameObject.tag == "TG2")
		{
			TS.task = true;
			TS.currentTask = "gas2";
		}
	}
}
