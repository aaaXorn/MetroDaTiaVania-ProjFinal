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
	
	int inocenAlive = 4;
	int sabotaAlive = 2;
	
	int randomRole;
	
	bool role1, role2, role3, role4, role5, role6;
	
    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
		if(inocenAlive<=0)
		{
			SceneManager.LoadScene("Level3");
		}
		
		if(sabotaAlive<=0)
		{
			SceneManager.LoadScene("Level3");
		}
    }
	
	public void SetRoles()//aviso! o void a seguir pode causar dor de cabeça 
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
	
	//pras roles funcionarem, ler pode causar possiveis danos na medula espinhal, entre outros sintomas
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
}
