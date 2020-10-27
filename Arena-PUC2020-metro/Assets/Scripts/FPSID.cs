using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FPSID : MonoBehaviour
{
    public TextMesh myname;
    public PhotonView pview;
    // Start is called before the first frame update
    void Start()
    {
        myname.text = pview.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        myname.transform.forward = transform.position - Camera.main.transform.position;

    }
}
