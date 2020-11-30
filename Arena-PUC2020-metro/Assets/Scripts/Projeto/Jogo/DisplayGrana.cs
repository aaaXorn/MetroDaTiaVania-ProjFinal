using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGrana : MonoBehaviour
{
	public GameObject Player;
	public PlayerAmong PA;
	
	public Text Txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Txt.text = PA.money.ToString();
    }
}
