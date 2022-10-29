using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	Vector2 mousePos;
	[SerializeField] Transform firepoint;
	[SerializeField] GameObject fireBallPrefab;
	[SerializeField] float bulletSpeed;
	Player player;
	void Start()
	{
		player = GetComponent<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//shoot


		if (Input.GetKeyDown(KeyCode.Mouse0) && player.ammo > 0)
		{
			Shoot();
		}
	}

	void Shoot()
	{
		Vector2 shootDir = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
		Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg);
		GameObject fireBall = Instantiate(fireBallPrefab, firepoint.position, rot);
		fireBall.GetComponent<Rigidbody2D>().AddForce(shootDir * bulletSpeed);
		fireBall.GetComponent<FireBall>().dmg = player.dmg;
		player.ammo--;
		player.UpdateHud();
	}
}
