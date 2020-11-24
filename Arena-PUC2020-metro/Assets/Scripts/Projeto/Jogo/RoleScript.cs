using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;

public class RoleScript : MonoBehaviour
{
	PhotonView pView;
	
	public GameObject Player;
	public PlayerAmong PA;
	
	public GameObject Cabeca;
	public PlayerCabeca PC;
	
	public int players;
	int sabotadores = 1;
	
	int inocenAlive = 4;
	int sabotaAlive = 2;
	
	//bool randomize = false;
	int randomRole;
	
	[SerializeField]
	int randomSkin;
	[SerializeField]
	bool skin1, skin2, skin3, skin4, skin5, skin6, skin7, skin8, skin9, skin10;
	
	int numeroInocen;
	int numeroSabota;
	//bool inocente;
	
	bool start;
	float startTimer = 6;
	
    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
		if(numeroInocen+numeroSabota==players && start == false)
		{
			if(startTimer>0)
				startTimer -= Time.deltaTime;
			else
			{
				PA.mayBeAttacked = true;
				inocenAlive = numeroInocen;
				sabotaAlive = numeroSabota;
				start = true;
			}
		}
		
		if(inocenAlive<=0)
		{
			SceneManager.LoadScene("Level3");
		}
		
		if(sabotaAlive<=0)
		{
			SceneManager.LoadScene("Level3");
		}
		
		if(players>5)
			sabotadores = 2;
    }
	
	public void SetSkin()//usar pra cabeça dos players
	{
		randomSkin = Random.Range(0, 10);//0 a 9, o 2o numero do int Random.Range e exclusive
		
		switch(randomSkin)
		{
			case 0:
			if(skin1 == false)
			{
				PA.skinOK = true;
				PC.skin = 1;
				pView.RPC("RPC_skin1", RpcTarget.All);
			}
			break;
			
			case 1:
			if(skin2 == false)
			{
				PA.skinOK = true;
				PC.skin = 2;
				pView.RPC("RPC_skin2", RpcTarget.All);
			}
			break;
			
			case 2:
			if(skin3 == false)
			{
				PA.skinOK = true;
				PC.skin = 3;
				pView.RPC("RPC_skin3", RpcTarget.All);
			}
			break;
			
			case 3:
			if(skin4 == false)
			{
				PA.skinOK = true;
				PC.skin = 4;
				pView.RPC("RPC_skin4", RpcTarget.All);
			}
			break;
			
			case 4:
			if(skin5 == false)
			{
				PA.skinOK = true;
				PC.skin = 5;
				pView.RPC("RPC_skin5", RpcTarget.All);
			}
			break;
			
			case 5:
			if(skin6 == false)
			{
				PA.skinOK = true;
				PC.skin = 6;
				pView.RPC("RPC_skin6", RpcTarget.All);
			}
			break;
			
			case 6:
			if(skin7 == false)
			{
				PA.skinOK = true;
				PC.skin = 7;
				pView.RPC("RPC_skin7", RpcTarget.All);
			}
			break;
			
			case 7:
			if(skin8 == false)
			{
				PA.skinOK = true;
				PC.skin = 8;
				pView.RPC("RPC_skin8", RpcTarget.All);
			}
			break;
			
			case 8:
			if(skin9 == false)
			{
				PA.skinOK = true;
				PC.skin = 9;
				pView.RPC("RPC_skin9", RpcTarget.All);
			}
			break;
			
			case 9:
			if(skin10 == false)
			{
				PA.skinOK = true;
				PC.skin = 10;
				pView.RPC("RPC_skin10", RpcTarget.All);
			}
			break;
			
			default:
			break;
		}
	}
	
	public void SetRoles()
	{
		randomRole = Random.Range(0, 3);

		if(randomRole<2)
		{
			if(numeroInocen<players-sabotadores)
			{
				//inocente = true;
				PA.role = "inocente";
				pView.RPC("RPC_numeroInocenMais", RpcTarget.All);
				PA.RoleCall();
			}
		}
		else
		{
			if(numeroSabota<sabotadores)
			{
				//inocente = false;
				PA.role = "sabotador";
				pView.RPC("RPC_numeroSabotaMais", RpcTarget.All);
				PA.RoleCall();
			}
		}
	}
	
	//funcoes pro PA usar, colocadas aqui pra ficar mais facil de encontrar
	public void maisPlayer()
	{
		pView.RPC("RPC_maisPlayer", RpcTarget.All);
	}
	[PunRPC]
	void RPC_maisPlayer()
	{
		players++;
	}
	
	public void inocenMorre()
	{
		pView.RPC("RPC_inocenMorre", RpcTarget.All);
	}
	[PunRPC]
	void RPC_inocenMorre()
	{
		inocenAlive--;
	}
	
	public void sabotaMorre()
	{
		pView.RPC("RPC_sabotaMorre", RpcTarget.All);
	}
	[PunRPC]
	void RPC_sabotaMorre()
	{
		sabotaAlive--;
	}
	
	//funcoes de RPC pras roles funcionarem
	[PunRPC]
	void RPC_skin1()
	{
		skin1 = true;
	}
	[PunRPC]
	void RPC_skin2()
	{
		skin2 = true;
	}
	[PunRPC]
	void RPC_skin3()
	{
		skin3 = true;
	}
	[PunRPC]
	void RPC_skin4()
	{
		skin4 = true;
	}
	[PunRPC]
	void RPC_skin5()
	{
		skin5 = true;
	}
	[PunRPC]
	void RPC_skin6()
	{
		skin6 = true;
	}
	[PunRPC]
	void RPC_skin7()
	{
		skin7 = true;
	}
	[PunRPC]
	void RPC_skin8()
	{
		skin8 = true;
	}
	[PunRPC]
	void RPC_skin9()
	{
		skin9 = true;
	}
	[PunRPC]
	void RPC_skin10()
	{
		skin10 = true;
	}
	
	[PunRPC]
	void RPC_numeroInocenMais()
	{
		numeroInocen++;
	}
	[PunRPC]
	void RPC_numeroSabotaMais()
	{
		numeroSabota++;
	}
}
