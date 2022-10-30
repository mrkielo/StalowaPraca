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
	[SerializeField] public Transform bow;
	[SerializeField] Animator bowAnimator;
	[SerializeField] float shootDelay;
	float lastShoot;
	void Start()
	{
		player = GetComponent<Player>();
		lastShoot = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 shootDir = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
		Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg);
		bow.rotation = rot;

		//shoot


		if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot + shootDelay < Time.time && player.ammo > 0)
		{
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot()
	{
		bowAnimator.SetTrigger("Shoot");
		yield return new WaitForSeconds(0.1f);
		Vector2 shootDir = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
		GameObject fireBall = Instantiate(fireBallPrefab, firepoint.position, bow.rotation);
		fireBall.GetComponent<Rigidbody2D>().AddForce(shootDir * bulletSpeed, ForceMode2D.Impulse);
		fireBall.GetComponent<FireBall>().dmg = player.dmg;
		player.ammo--;
		player.UpdateHud();
		lastShoot = Time.time;
	}
}
