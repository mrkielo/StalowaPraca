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
	[Header("Sword")]
	[SerializeField] float meeleDelay;
	[SerializeField] float swordDmg;
	[SerializeField] SpriteRenderer swordSprite;
	[SerializeField] Transform meeleBoxOrigin;
	[SerializeField] Vector2 meeleBoxSize;
	[SerializeField] Animator swordAnimator;
	float lastSword;
	float lastShoot;
	void Start()
	{
		player = GetComponent<Player>();
		lastShoot = Time.time;
		lastSword = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 shootDir = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
		Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg);
		bow.rotation = rot;

		//shoot
		if (Input.GetKeyDown(KeyCode.Mouse1) && lastSword + meeleDelay < Time.time)
		{
			swordSprite.enabled = true;
			bowAnimator.GetComponent<SpriteRenderer>().enabled = false;
			SwordAttack();
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot + shootDelay < Time.time && player.ammo > 0)
		{
			swordSprite.enabled = false;
			bowAnimator.GetComponent<SpriteRenderer>().enabled = true;
			StartCoroutine(Shoot());
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawCube(meeleBoxOrigin.position, new Vector3(meeleBoxSize.x, meeleBoxSize.y, 1));
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

	void SwordAttack()
	{
		swordAnimator.SetTrigger("Attack");
		Collider2D enemy = Physics2D.OverlapBox(meeleBoxOrigin.position, meeleBoxSize, 0, LayerMask.GetMask("Enemy"));
		if (enemy != null)
		{
			Debug.Log(enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(swordDmg);
		}
		lastSword = Time.time;
	}
}
