using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCameraScript : MonoBehaviour
{
	CinemachineVirtualCamera vCamera;
	
	public Transform followPosition;
    // Start is called before the first frame update
    void Start()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void CameraFollow(GameObject player)
	{
		followPosition = player.transform;
		vCamera.LookAt = followPosition;
		vCamera.Follow = followPosition;
	}
}
