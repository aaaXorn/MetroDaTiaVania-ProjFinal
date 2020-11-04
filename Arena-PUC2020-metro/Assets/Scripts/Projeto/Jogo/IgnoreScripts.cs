using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IgnoreScripts : MonoBehaviour
{
	[SerializeField]
	MonoBehaviour[] scriptsIgnorar;
	PhotonView photonview;
	
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
		if(!photonview.IsMine)
		{
			foreach(MonoBehaviour scrpt in scriptsIgnorar)
				scrpt.enabled = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}