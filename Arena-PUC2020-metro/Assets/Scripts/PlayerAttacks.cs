using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
	[SerializeField]
	GameObject bulletPrefab, bullet;
	[SerializeField]
	PlayerAmong PA;
	[SerializeField]
	float bulletSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.rotation = Quaternion.Euler(0, 0, PA.directionZ);
		
        if(Input.GetMouseButtonDown(0))
		{
			PA.cursorMagnitude = PA.cursorDistance.magnitude;
			PA.attackDirection = -1 * PA.cursorDistance / PA.cursorMagnitude;//direçao do tiro, -1 * para corrigir
			PA.attackDirection.Normalize();//faz o valor ser 1, mas mantem a direçao
			Shoot();
		}
    }
	
	void Shoot()
	{
		bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, PA.directionZ));
		bullet.GetComponent<Rigidbody2D>().velocity = PA.attackDirection * bulletSpeed;
	}
}
