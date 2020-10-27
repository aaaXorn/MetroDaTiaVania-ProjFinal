using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public Rigidbody2D rdb;
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            rdb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * 10);
        }
    }
}
