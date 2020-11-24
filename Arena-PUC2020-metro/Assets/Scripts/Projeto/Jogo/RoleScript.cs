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
	
	public int players;
	int sabotadores = 1;
	
	int inocenAlive = 4;
	int sabotaAlive = 2;
	
	bool randomize = false;
	int randomRole;
	
	bool role1, role2, role3, role4, role5, role6;
	
	int numeroInocen;
	int numeroSabota;
	bool inocente;
	
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
	
	/*public void SetRoles()//usar pra cabeça dos players
	{
		randomRole = Random.Range(0, 6);//0 a 5, o 2o numero do int Random.Range e exclusive
		
		switch(randomRole)
		{
			case 0:
			if(role1)
				randomRole = Random.Range(0, 6);
			else
			{
				PA.role = "inocente";
				pView.RPC("RPC_role1", RpcTarget.All);
				PA.RoleCall();
			}
			break;
			
			case 1:
			if(role2)
				randomRole = Random.Range(0, 6);
			else
			{
				PA.role = "inocente";
				pView.RPC("RPC_role2", RpcTarget.All);
				PA.RoleCall();
			}
			break;
			
			case 2:
			if(role3)
				randomRole = Random.Range(0, 6);
			else
			{
				PA.role = "inocente";
				pView.RPC("RPC_role3", RpcTarget.All);
				PA.RoleCall();
			}
			break;
			
			case 3:
			if(role4)
				randomRole = Random.Range(0, 6);
			else
			{
				PA.role = "inocente";
				pView.RPC("RPC_role4", RpcTarget.All);
				PA.RoleCall();
			}
			break;
			
			case 4:
			if(role5)
				randomRole = Random.Range(0, 6);
			else
			{
				PA.role = "sabotador";
				pView.RPC("RPC_role5", RpcTarget.All);
				PA.RoleCall();
			}
			break;
			
			case 5:
			if(role6)
				randomRole = Random.Range(0, 6);
			else
			{
				PA.role = "sabotador";
				pView.RPC("RPC_role6", RpcTarget.All);
				PA.RoleCall();
			}
			break;
			
			default:
			randomRole = Random.Range(0, 6);
			break;
		}
	}*/
	
	public void SetRoles()
	{
		randomRole = Random.Range(0, 3);

		if(randomRole<2)
		{
			if(numeroInocen<players-sabotadores)
			{
				inocente = true;
				PA.role = "inocente";
				pView.RPC("RPC_numeroInocenMais", RpcTarget.All);
				PA.RoleCall();
			}
		}
		else
		{
			if(numeroSabota<sabotadores)
			{
				inocente = false;
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
	void RPC_role1()
	{
		role1 = true;
	}
	[PunRPC]
	void RPC_role2()
	{
		role2 = true;
	}
	[PunRPC]
	void RPC_role3()
	{
		role3 = true;
	}
	[PunRPC]
	void RPC_role4()
	{
		role4 = true;
	}
	[PunRPC]
	void RPC_role5()
	{
		role5 = true;
	}
	[PunRPC]
	void RPC_role6()
	{
		role6 = true;
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
