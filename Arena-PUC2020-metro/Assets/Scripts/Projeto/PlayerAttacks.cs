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
	GameObject bulletPrefab, bullet;
	[SerializeField]
	GameObject RoboPlayer;
	[SerializeField]
	PlayerAmong PA;
	
	float attackTimer = 0.1f;
	bool attackStart = false;
	float attackCD = 1;
	
	float bulletSpeed = 10;
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
						pView.RPC("RPC_Shoot", RpcTarget.All);
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
		}
    }

	void Aim()//direcao do ataque com base na posicao do cursor
	{
		PA.cursorMagnitude = PA.cursorDistance.magnitude;
		PA.attackDirection = -1 * PA.cursorDistance / PA.cursorMagnitude;//direçao do tiro, "-1 *" para corrigir
		PA.attackDirection.Normalize();//faz o valor ser 1, mas mantem a direçao
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
		//bullet = PhotonNetwork.Instantiate("bullet0", transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		//ver com PhotonNetwork, causou bugs entao nao esta sendo usada
		anim.SetTrigger("Pew");
		bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		bullet.transform.Translate(-3.35f, 0, 0);//para o tiro não spawnar dentro do player, negativo pra ir pro lado certo
		bullet.GetComponent<Rigidbody2D>().velocity = PA.attackDirection * bulletSpeed;
	}
}
