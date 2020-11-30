using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCabeca : MonoBehaviour//PunCallbacks, IPunObservable
{
	Animator anim;
	SpriteRenderer sRender;
	PhotonView pView;
	
	[SerializeField]
	GameObject RoleSet;
	[SerializeField]
	RoleScript RS;
	
	public int skin;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		sRender = GetComponent<SpriteRenderer>();
		pView = GetComponent<PhotonView>();
		
		if(pView.IsMine)
		{
			RoleSet = GameObject.FindWithTag("RoleSet");
			RS = RoleSet.GetComponent<RoleScript>();
			
			RS.Cabeca = gameObject;
			RS.PC = RS.Cabeca.GetComponent<PlayerCabeca>();
		}
    }

	#region IPunObservable implementation
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.IsWriting && pView.IsMine)//manda as variaveis pros outros jogadores
		{
			stream.SendNext(skin);
		}
		else//recebe as variaveis
		{
			skin = (int)stream.ReceiveNext();
		}
	}
	
	#endregion
	
    // Update is called once per frame
    void Update()
    {
		if(pView.IsMine)
			anim.SetInteger("cabeca", skin);
    }
	
	public void OnDeath()
	{
		sRender.enabled = false;
	}
}
