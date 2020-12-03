using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttacks : MonoBehaviour
{
	PhotonView pView;
	
	Animator anim;
	SpriteRenderer sRender;
	
	[SerializeField]
	GameObject bulletPrefab, Ataque, swordPrefab, taserPrefab, imaPrefab, shieldPrefab;
	[SerializeField]
	GameObject RoboPlayer;
	[SerializeField]
	PlayerAmong PA;
	
	float attackTimer = 0.1f;
	bool attackStart = false;
	float attackCD = 1;
	
	bool shieldUse;
	float shieldCD = 7;
	
	[SerializeField]
	int armaUsada = 1;
	public int arma1, arma2, arma3, arma4, arma5;
	
	float bulletSpeed = 11;
	[SerializeField]
	Vector2 MovimentoArma;
    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
		anim = GetComponent<Animator>();
		sRender = GetComponent<SpriteRenderer>();
		
		RoboPlayer = transform.parent.gameObject;//faz a variavel RoboPlayer ser o objeto parente de attackDirection
		PA = RoboPlayer.GetComponent<PlayerAmong>();
    }

    // Update is called once per frame
    void Update()
    {
		if (pView.IsMine && PA.alive)
		{
			pView.RPC("RPC_Rotation", RpcTarget.All);
			anim.SetInteger("ArmaUsada", armaUsada);
			
			MovimentoArma = PA.Movimento;
			
			if(PA.mayAttack && attackStart == false)
			{
				if(Input.GetMouseButtonDown(0))
				{
					Aim();
					PA.shoot = true;
				}
				
				if(PA.shoot)
				{
					if(attackTimer<=0)
					{
						anim.SetTrigger("Pew");
						Ataques();
						attackTimer = 0.1f;
						PA.shoot = false;
						attackStart = true;
					}
					else
					{
						attackTimer -= Time.deltaTime;
					}
				}
			}
			if(attackStart == true)
			{
				attackCD -= Time.deltaTime;
				if(attackCD<=0)
				{
					attackStart = false;
					attackCD = 1;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha1))
				armaUsada = arma1;
			else if(Input.GetKeyDown(KeyCode.Alpha2))
				armaUsada = arma2;
			else if(Input.GetKeyDown(KeyCode.Alpha3))
				armaUsada = arma3;
			else if(Input.GetKeyDown(KeyCode.Alpha4))
				armaUsada = arma4;
			else if(Input.GetKeyDown(KeyCode.Alpha5))
				armaUsada = arma5;
				
			if(shieldUse)
			{
				shieldCD -= Time.deltaTime;
				if(shieldCD<=0)
				{
					shieldUse = false;
					shieldCD = 6;
				}
			}
		}
		else if(PA.alive == false)
			pView.RPC("RPC_SpriteDisable", RpcTarget.All);
    }

	void Aim()//direcao do ataque com base na posicao do cursor
	{
		PA.cursorMagnitude = PA.cursorDistance.magnitude;
		PA.attackDirection = -1 * PA.cursorDistance / PA.cursorMagnitude;//direçao do tiro, "-1 *" para corrigir
		PA.attackDirection.Normalize();//faz o valor ser 1, mas mantem a direçao
	}

	void Ataques()
	{
		switch(armaUsada)
		{
			case 1:
			pView.RPC("RPC_Shoot", RpcTarget.All);
			break;
				
			case 2:
			pView.RPC("RPC_Sword", RpcTarget.All);
			break;
			
			case 3:
			pView.RPC("RPC_Taser", RpcTarget.All);
			break;
			
			case 4:
			pView.RPC("RPC_Ima", RpcTarget.All);
			break;
			
			case -1:
			if(shieldUse == false)
			{
				shieldUse = true;
				pView.RPC("RPC_Shield", RpcTarget.All);
			}
			break;
			
			default:
			break;
		}
	}

	//funcoes do RPC
	[PunRPC]
	void RPC_Rotation()//rotaciona o objeto attackDirection para ele apontar pro cursor
	{
		transform.rotation = Quaternion.Euler(0, 0, PA.directionZ);
		if(PA.cursorDistance.x<0)
		{
			sRender.flipY = true;
		}
		else
		{
			sRender.flipY = false;
		}
	}
	
	[PunRPC]
	void RPC_Shoot()//atira um prefab na direcao do cursor
	{
		//Ataque = PhotonNetwork.Instantiate("bullet0", transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		//versao com PhotonNetwork, causou bugs entao nao esta sendo usada
		Ataque = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		Ataque.transform.Translate(-3.35f, 0, 0);//para o tiro não spawnar dentro do player, negativo pra ir pro lado certo
		Ataque.GetComponent<Rigidbody2D>().velocity = PA.attackDirection * bulletSpeed;
	}
	
	[PunRPC]
	void RPC_Sword()
	{
		Ataque = Instantiate(swordPrefab, transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		Ataque.transform.Translate(-4.65f, 0, 0);
		Ataque.GetComponent<Rigidbody2D>().velocity = MovimentoArma;
	}
	
	[PunRPC]
	void RPC_Taser()
	{
		Ataque = Instantiate(taserPrefab, transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		Ataque.transform.Translate(-3.75f, 0, 0);
		Ataque.GetComponent<Rigidbody2D>().velocity = MovimentoArma;
	}
	
	[PunRPC]
	void RPC_Ima()
	{
		Ataque = Instantiate(imaPrefab, transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		Ataque.transform.Translate(-3.35f, 0, 0);
		Ataque.GetComponent<Rigidbody2D>().velocity = MovimentoArma;
	}
	
	[PunRPC]
	void RPC_Shield()
	{
		Ataque = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
		Ataque.GetComponent<Rigidbody2D>().velocity = MovimentoArma;
	}
	
	[PunRPC]
	void RPC_SpriteDisable()
	{
		sRender.enabled = false;
	}
}
